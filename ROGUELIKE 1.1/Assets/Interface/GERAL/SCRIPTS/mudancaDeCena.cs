using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mudancaDeCena : MonoBehaviour
{
    public Animator animacaoDeTransicao;
    public string nomeDaCena;
    public float tempoDeTransicao;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MudançaDeCena()
    {
        //Time.timeScale = 1f; (caso o timeScale continue zero, quando voltar pra o jogo, dps de voltar ao menu)(em observacao...)
        StartCoroutine(CarregarCena());
    }
    public void FecharJogo()
    {
        StartCoroutine(JogoFechando());
        
    }
    private IEnumerator JogoFechando()
    {
        yield return new WaitForSeconds(tempoDeTransicao);
        Application.Quit();
    }

    private IEnumerator CarregarCena()
    {
        // animacaoDeTransicao.SetTrigger("end");
        yield return new WaitForSeconds(tempoDeTransicao);
        print(SceneManager.sceneCount);
        SceneManager.LoadScene(nomeDaCena, LoadSceneMode.Single);
    }
}
