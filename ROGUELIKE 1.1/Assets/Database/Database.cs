using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Database : MonoBehaviour
{

    public List<GameObject> PrefabsInimigos = new List<GameObject>();
    public List<GameObject> PrefabsItens = new List<GameObject>();
    private string prefixoItem = "i.";



    // Start is called before the first frame update
    void Start()
    {   //Adaptar para o nome do arquivo ser o id, e ele carregar por meio de um for, que ao entrar como parametro usa .ToString() para converter para string.
        //Inicializando Inimigos a partir do nome:
        bool loop = true;
        int i = 0;
        while (loop == true)
        {
            loop = AdicionarInimigo(i.ToString());
            i++;
        }
        //Inicializando Itens a partir do nome:

        bool loop2 = true;
        int j = 0;
        while (loop2 == true)
        {
            loop2 = AdicionarItem(prefixoItem + j.ToString());
            j++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool AdicionarInimigo(string nomeDoArquivo)
    {
        //nome é o nome do prefab e não o nome visivel no jogo.
        //o id será igual a posicao na lista.
        GameObject inimigoEncontrado = null;

        if ((GameObject)Resources.Load(nomeDoArquivo))
        {
            this.PrefabsInimigos.Add((GameObject)Resources.Load(nomeDoArquivo));
        }
        else
        {
            return false;
        }

        foreach (GameObject inimigo in PrefabsInimigos)
        {
            if (inimigo.name == nomeDoArquivo)
            {
                inimigoEncontrado = inimigo;
            }
        }
        int index = PrefabsInimigos.IndexOf(inimigoEncontrado);
        this.PrefabsInimigos[index].GetComponent<Inimigo>().index = PrefabsInimigos.IndexOf(inimigoEncontrado);
        this.PrefabsInimigos[index].GetComponent<Inimigo>().id = Convert.ToInt32(inimigoEncontrado.name);

        return true;
    }

    private bool AdicionarItem(string nomeDoArquivo)
    {
        //nome é o nome do prefab e não o nome visivel no jogo.
        //o id será igual a posicao na lista.
        GameObject itemEncontrado = null;

        if ((GameObject)Resources.Load(nomeDoArquivo))
        {
            this.PrefabsItens.Add((GameObject)Resources.Load(nomeDoArquivo));
        }
        else
        {
            return false;
        }

        foreach (GameObject item in PrefabsItens)
        {
            if (item.name == nomeDoArquivo)
            {
                itemEncontrado = item;
            }
        }
        int index = PrefabsItens.IndexOf(itemEncontrado);
        this.PrefabsItens[index].GetComponent<Item>().index = PrefabsItens.IndexOf(itemEncontrado);
        this.PrefabsItens[index].GetComponent<Item>().id = Convert.ToInt32(itemEncontrado.name.Replace(prefixoItem, ""));

        return true;
    }

    public static GameObject BuscarInimigo(int fase, int raridade)
    {
        Database scriptDatabase = FindObjectOfType<Database>() as Database;
        System.Random rnd;
        List<GameObject> inimigosValidos = null;
        inimigosValidos = new List<GameObject>();

        foreach (GameObject inimigo in scriptDatabase.PrefabsInimigos)
        {
            Inimigo scriptInimigo = inimigo.GetComponent<Inimigo>();
            if (scriptInimigo.fase == fase && scriptInimigo.raridade == raridade)
            {
                inimigosValidos.Add(inimigo);
            }
        }
        rnd = new System.Random(Guid.NewGuid().GetHashCode());
        int indiceInimigo = rnd.Next(0, inimigosValidos.Count - 1);
        GameObject inimigoSpawnado = inimigosValidos[indiceInimigo];

        return inimigoSpawnado;
    }

    public static GameObject BuscarItem(int tipo, int raridade)
    {
        Database scriptDatabase = FindObjectOfType<Database>() as Database;
        System.Random rnd;
        List<GameObject> itensValidos = null;
        itensValidos = new List<GameObject>();

        foreach (GameObject item in scriptDatabase.PrefabsItens)
        {
            Item scriptItem = item.GetComponent<Item>();
            if (scriptItem.tipo == tipo && scriptItem.raridade == raridade)
            {
                itensValidos.Add(item);
            }
        }
        rnd = new System.Random(Guid.NewGuid().GetHashCode());
        int indiceItem = rnd.Next(0, itensValidos.Count - 1);
        GameObject itemSpawnado = itensValidos[indiceItem];

        return itemSpawnado;
    }
}
