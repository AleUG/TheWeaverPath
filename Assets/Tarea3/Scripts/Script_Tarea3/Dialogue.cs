using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Dialogue : MonoBehaviour
{
    private bool isPlayerRange = false;
    private bool isDialogueInProgress = false;
    private bool isWriting = false;

    [SerializeField] private GameObject teclaInteractuar;
    [SerializeField] private GameObject teclaContinue;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip typeSound;

    public GameObject llave;

    private BoxCollider2D boxCollider2D;

    private int lineIndex;
    private Coroutine typingCoroutine;

    private float typingTime = 0.05f;

    [System.Serializable]
    public class DialogueGroup
    {
        [TextArea(4, 6)] public string[] dialogueLines;
    }

    public DialogueGroup[] dialogueGroups;

    private int currentGroupIndex; // Índice del grupo actual

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (isPlayerRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isDialogueInProgress)
            {
                StartDialogue();
            }
            else if (isWriting)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = dialogueGroups[currentGroupIndex].dialogueLines[lineIndex];
                isWriting = false;
                isPlayerRange = true;
                teclaInteractuar.SetActive(false);
                teclaContinue.SetActive(true);
            }
            else
            {
                if (lineIndex < dialogueGroups[currentGroupIndex].dialogueLines.Length - 1)
                {
                    NextDialogueLine();
                }
                else
                {
                    EndDialogue();
                }
            }
        }
    }

    private void StartDialogue()
    {
        isDialogueInProgress = true;
        isPlayerRange = false;
        teclaInteractuar.SetActive(false);
        dialoguePanel.SetActive(true);
        dialogueText.text = string.Empty;
        boxCollider2D.enabled = false;

        PlayerMove playerMove = FindObjectOfType<PlayerMove>();
        if (playerMove != null)
        {
            playerMove.CanMove = false;
        }

        lineIndex = 0; // Iniciar con el primer elemento del grupo actual

        typingCoroutine = StartCoroutine(ShowLines());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        dialogueText.text = string.Empty;
        typingCoroutine = StartCoroutine(ShowLines());

        if (dialoguePanel == true);
        {
            PlayerMove playerMove = FindObjectOfType<PlayerMove>();
            if (playerMove != null)
            {
                playerMove.CanMove = false;
            }
        }
    }

    private IEnumerator ShowLines()
    {
        isWriting = true;
        teclaContinue.SetActive(false);

        foreach (char ch in dialogueGroups[currentGroupIndex].dialogueLines[lineIndex])
        {
            dialogueText.text += ch;

            if (typeSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(typeSound);
            }

            yield return new WaitForSeconds(typingTime);
        }

        isWriting = false;

        PlayerMove playerMove = FindObjectOfType<PlayerMove>();
        if (playerMove != null && lineIndex == dialogueGroups[currentGroupIndex].dialogueLines.Length - 1)
        {
            playerMove.CanMove = true;
            playerMove.canJump = true;
        }

        isPlayerRange = true;
        teclaInteractuar.SetActive(false);

        if (!isWriting)
        {
            teclaContinue.SetActive(true);
        }

        if (dialoguePanel == true) ;
        {
            playerMove.CanMove = false;
        }

        typingCoroutine = null;
    }

    private void EndDialogue()
    {
        isDialogueInProgress = false;
        dialoguePanel.SetActive(false);
        teclaInteractuar.SetActive(true);
        teclaContinue.SetActive(false);
        isPlayerRange = false;
        boxCollider2D.enabled = true;

        PlayerMove playerMove = FindObjectOfType<PlayerMove>();
        if (playerMove != null)
        {
            playerMove.CanMove = true;
            playerMove.canJump = true;
        }

        if (llave != null) // Añade esta condición para verificar si el objeto llave existe
        {
            llave.SetActive(true);
        }

        if (currentGroupIndex < dialogueGroups.Length - 1)
        {
            currentGroupIndex++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerRange = true;
            teclaInteractuar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerRange = false;
            teclaInteractuar.SetActive(false);
        }
    }
}
