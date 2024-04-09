using UnityEngine;

public class MenuActions : MonoBehaviour
{
    public GameObject menuPanel; // Viite menu paneeliin

    void Start()
    {
        // Varmista, ett‰ menu paneeli on piilotettu pelin alussa
        menuPanel.SetActive(false);
    }

    public void ToggleMenu()
    {
        // Vaihtaa menu paneelin n‰kyvyyden
        menuPanel.SetActive(!menuPanel.activeSelf);
        Time.timeScale = menuPanel.activeSelf ? 0 : 1; // Pys‰ytt‰‰ tai jatkaa pelin aikaa
    }

    public void ContinueGame()
    {
        // Piilottaa menu paneelin ja jatkaa peli‰
        menuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        // Sulkee sovelluksen
        Application.Quit();
    }
}