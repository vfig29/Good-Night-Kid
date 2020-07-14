using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class Grafico : MonoBehaviour
{
    public static int idGrafico;
    public GameObject tileParede, tileParedeLado, tileParedeCorner, tileParedeInner, tileParedeDiagonal, tilePsala, tilePsalaInner, tileChao, tilePorta, tileComplementoPorta1, tileComplementoPorta2, tileEntrada, tileComplementoEntrada1, tileComplementoEntrada2, tileContorno, tileContornoCorner, tileCaminho, tilePsalaCorner, individual, tileAntiExploit, tileEntradaSecreta;
    public float tileSize = 2.0f;
    public GameObject spawn, bossaltar, bossback;
    public GameObject cameraBound, cameraBoundInstanciada;
    public BoxCollider2D cameraBoundCollider;
    private GameObject instanciaEntrada, instanciaPorta;
    private Entrada scriptEntrada;
    private Porta scriptPorta;
    public static Vector3 tileCoord;
    public static bool permitSpawn = false;
    private Atributos scriptAtributos;
    public Zona zonaCriada;

    public Grafico()
    {

    }

    public void gerarGrafico(Area areaPai, int linha, int coluna)
    {
        Dungeon dungeonInstanciada = areaPai.dungeonInserida;
        Vector3 boundsmin = new Vector3(0,0,0), boundsmax = new Vector3(0, 0, 0);
        //inicio do teste no console do sistema de dificuldade de salas e areas
        if (areaPai.boss == false)
        {
            print("x: " + areaPai.posicaoMatriz.x + " / y: " + areaPai.posicaoMatriz.y + " dificuldade: " + areaPai.dificuldade);
            for (int i = 0; i < areaPai.dungeonInserida.numeroSalas; i++)
            {
                print("x: " + areaPai.posicaoMatriz.x + " / y: " + areaPai.posicaoMatriz.y + " idsala: " + areaPai.dungeonInserida.salas[i].idSala + " dificuldade: " + areaPai.dungeonInserida.salas[i].dificuldade);
            }
            
        }
        //fim do teste no console do sistema de dificuldade de salas e areas
        for (int i = 0; i < dungeonInstanciada.tamanhoDungeon.x; i++)
        {
            for (int j = 0; j < dungeonInstanciada.tamanhoDungeon.y; j++)
            {

                Vetor2 v = new Vetor2(0, 0);
                Celula cel = new Celula(v, " ");
                cel = dungeonInstanciada.dungeon[i, j];
                tileCoord = new Vector3(linha*2*Config.tamanhoMaxDungeon.x*tileSize + cel.coordCelulaDun.x*tileSize, -1*coluna*2* Config.tamanhoMaxDungeon.y*tileSize + cel.coordCelulaDun.y*-1*tileSize, 0); //eixo y está invertido, pois o sistema de coordenadas que usei na matriz foi diferente. Invertendo ele renderiza nos lugares corretos da matriz//onde tem tileSize antes tinha um "2".
                gerarString(cel.tipoCelula, cel.subTipoCelula, tileCoord, cel.ehEntradaSecreta, cel.spawn, cel.spawnBoss, cel.spawnsbBoss, cel);
                if (i == 0 && j == dungeonInstanciada.tamanhoDungeon.y - 1)
                {
                    boundsmin.x = tileCoord.x;
                    boundsmin.y = tileCoord.y;

                }
                if (i == dungeonInstanciada.tamanhoDungeon.x - 1 && j == 0)
                {
                    Vector3 calibragemSize = new Vector3(0, 0), calibragemPosition = new Vector3(0, 0);
                    if (dungeonInstanciada.tamanhoDungeon.x % 2 == 0)
                    {
                        calibragemSize = new Vector3(tileSize, calibragemSize.y);
                        calibragemPosition = new Vector3(1, calibragemPosition.y);
                    }
                    if (dungeonInstanciada.tamanhoDungeon.x % 2 == 1)
                    {
                        calibragemSize = new Vector3(tileSize, calibragemSize.y);
                        calibragemPosition = new Vector3(calibragemPosition.x, calibragemPosition.y);

                    }
                    if (dungeonInstanciada.tamanhoDungeon.y % 2 == 0)
                    {
                        calibragemSize = new Vector3(calibragemSize.x, tileSize);
                        calibragemPosition = new Vector3(calibragemPosition.x, -1);

                    }
                    if (dungeonInstanciada.tamanhoDungeon.y % 2 == 1)
                    {
                        calibragemSize = new Vector3(calibragemSize.x, tileSize);
                        calibragemPosition = new Vector3(calibragemPosition.x, calibragemPosition.y);

                    }
                    boundsmax.x = tileCoord.x;
                    boundsmax.y = tileCoord.y;
                    cameraBoundInstanciada = Instantiate(cameraBound, tileCoord, Quaternion.identity);
                    cameraBoundCollider = cameraBoundInstanciada.GetComponent<BoxCollider2D>();
                    Bounds bounds = new Bounds();
                    bounds.SetMinMax(boundsmin, boundsmax);
                    cameraBoundCollider.bounds.center.Set(bounds.center.x, bounds.center.y, bounds.center.z);
                    cameraBoundCollider.size = bounds.size + calibragemSize;
                    cameraBoundCollider.offset = new Vector3(0, 0);
                    cameraBoundCollider.transform.position = new Vector3(linha * 2 * Config.tamanhoMaxDungeon.x * tileSize + ((dungeonInstanciada.tamanhoDungeon.x-1) / 2) * tileSize, -1 * coluna * 2 * Config.tamanhoMaxDungeon.y * tileSize + (-1*(dungeonInstanciada.tamanhoDungeon.y-1) / 2) * tileSize, 0) + calibragemPosition;
                }


            }
        }
        //posicionando tiles anti-exploit
        for (int i = -1; i <= dungeonInstanciada.tamanhoDungeon.x; i++)
        {
            for (int j = -1; j <= dungeonInstanciada.tamanhoDungeon.y; j++)
            {
                if ((i == -1 || j == -1) || (i == dungeonInstanciada.tamanhoDungeon.x || j == dungeonInstanciada.tamanhoDungeon.y))
                {
                    tileCoord = new Vector3(linha * 2 * Config.tamanhoMaxDungeon.x * 2 + i * 2, -1 * coluna * 2 * Config.tamanhoMaxDungeon.y * 2 + j * -2, 0); //eixo y está invertido, pois o sistema de coordenadas que usei na matriz foi diferente. Invertendo ele renderiza nos lugares corretos da matriz.
                    Instantiate(tileAntiExploit, tileCoord, Quaternion.identity);
                }
                

            }
        }

    }
    public void gerarString(string tipoCelula, string subTipoCelula, Vector3 tileCoord, bool ehEntradaSecreta, bool spawnPlayer, bool spawnBoss, bool spawnsbBoss,Celula cel)
    {
        if (tipoCelula == "parede")
        {
            if (subTipoCelula == "esquerda")
            {
                Instantiate(tileParedeLado, tileCoord, RotateClockwise());
            }
            if (subTipoCelula == "direita")
            {
                Instantiate(tileParedeLado, tileCoord, RotateAntiClockwise());
            }
            if (subTipoCelula == "cima")
            {
                
                Instantiate(tileParedeLado, tileCoord, RotateClockwiseTwice());
            }
            if (subTipoCelula == "baixo")
            {
                Instantiate(tileParedeLado, tileCoord, Quaternion.identity);
            }
            if (subTipoCelula == "quinaDC")
            {
                Instantiate(tileParedeCorner, tileCoord, RotateAntiClockwise());
            }
            if (subTipoCelula == "quinaEC")
            {
                Instantiate(tileParedeCorner, tileCoord, RotateClockwiseTwice());
            }
            if (subTipoCelula == "quinaDB")
            {
                
                Instantiate(tileParedeCorner, tileCoord, Quaternion.identity);
            }
            if (subTipoCelula == "quinaEB")
            {
                
                Instantiate(tileParedeCorner, tileCoord, RotateClockwise());
            }
            if (subTipoCelula == "centro")
            {
                Instantiate(tileParede, tileCoord, Quaternion.identity);
            }
            if (subTipoCelula == "individual")
            {
                Instantiate(individual, tileCoord, Quaternion.identity);
            }
            if (subTipoCelula == "innerDC")
            {
                Instantiate(tileParedeInner, tileCoord, RotateClockwiseTwice());
            }
            if (subTipoCelula == "innerEC")
            {
                Instantiate(tileParedeInner, tileCoord, RotateClockwise());
            }
            if (subTipoCelula == "innerDB")
            {
                
                Instantiate(tileParedeInner, tileCoord, RotateAntiClockwise());
            }
            if (subTipoCelula == "innerEB")
            {
                
                Instantiate(tileParedeInner, tileCoord, Quaternion.identity);
            }
            if (subTipoCelula == "diagonalDC")
            {
                Instantiate(tileParedeDiagonal, tileCoord, Quaternion.identity);
            }
            if (subTipoCelula == "diagonalEC")
            {
                Instantiate(tileParedeDiagonal, tileCoord, RotateAntiClockwise());
            }
        }
        if (tipoCelula == "chao")
        {
            Instantiate(tileChao, tileCoord, Quaternion.identity);
        }
        if (tipoCelula == "pSala")//
        {
            if (subTipoCelula == "esquerdap")
            {
                if (cel.ehComplementoPorta1 == true)
                {
                    Porta scriptComplemento = Instantiate(tileComplementoPorta1, tileCoord, RotateAntiClockwise()).GetComponent<Porta>();
                    scriptComplemento.temchave = cel.chave;
                    scriptComplemento.idSala = cel.idSalaPai;
                    scriptComplemento.areaPai = cel.dungeonPai.areaPai;
                }
                else if (cel.ehComplementoPorta2 == true)
                {
                    Porta scriptComplemento = Instantiate(tileComplementoPorta2, tileCoord, RotateAntiClockwise()).GetComponent<Porta>();
                    scriptComplemento.temchave = cel.chave;
                    scriptComplemento.idSala = cel.idSalaPai;
                    scriptComplemento.areaPai = cel.dungeonPai.areaPai;
                }
                else if(cel.ehComplementoPorta1 == false && cel.ehComplementoPorta2 == false)
                {
                    Instantiate(tilePsala, tileCoord, RotateAntiClockwise());   
                }
                
            }
            if (subTipoCelula == "direitap")
            {
                if (cel.ehComplementoPorta1 == true)
                {
                    Porta scriptComplemento = Instantiate(tileComplementoPorta2, tileCoord, RotateClockwise()).GetComponent<Porta>();
                    scriptComplemento.temchave = cel.chave;
                    scriptComplemento.idSala = cel.idSalaPai;
                    scriptComplemento.areaPai = cel.dungeonPai.areaPai;
                }
                else if (cel.ehComplementoPorta2 == true)
                {
                    Porta scriptComplemento = Instantiate(tileComplementoPorta1, tileCoord, RotateClockwise()).GetComponent<Porta>();
                    scriptComplemento.temchave = cel.chave;
                    scriptComplemento.idSala = cel.idSalaPai;
                    scriptComplemento.areaPai = cel.dungeonPai.areaPai;
                }
                else if (cel.ehComplementoPorta1 == false && cel.ehComplementoPorta2 == false)
                {
                    Instantiate(tilePsala, tileCoord, RotateClockwise());
                }

            }
            if (subTipoCelula == "cimap")
            {
                if (cel.ehComplementoPorta1 == true)
                {
                    Porta scriptComplemento = Instantiate(tileComplementoPorta2, tileCoord, Quaternion.identity).GetComponent<Porta>();
                    scriptComplemento.temchave = cel.chave;
                    scriptComplemento.idSala = cel.idSalaPai;
                    scriptComplemento.areaPai = cel.dungeonPai.areaPai;
                }
                else if (cel.ehComplementoPorta2 == true)
                {
                    Porta scriptComplemento = Instantiate(tileComplementoPorta1, tileCoord, Quaternion.identity).GetComponent<Porta>();
                    scriptComplemento.temchave = cel.chave;
                    scriptComplemento.idSala = cel.idSalaPai;
                    scriptComplemento.areaPai = cel.dungeonPai.areaPai;
                }
                else if (cel.ehComplementoPorta1 == false && cel.ehComplementoPorta2 == false)
                {
                    Instantiate(tilePsala, tileCoord, Quaternion.identity);
                }
            }
            if (subTipoCelula == "baixop")
            {
                if (cel.ehComplementoPorta1 == true)
                {
                    Porta scriptComplemento = Instantiate(tileComplementoPorta1, tileCoord, RotateAntiClockwiseTwice()).GetComponent<Porta>();
                    scriptComplemento.temchave = cel.chave;
                    scriptComplemento.idSala = cel.idSalaPai;
                    scriptComplemento.areaPai = cel.dungeonPai.areaPai;
                }
                else if (cel.ehComplementoPorta2 == true)
                {
                    Porta scriptComplemento = Instantiate(tileComplementoPorta2, tileCoord, RotateAntiClockwiseTwice()).GetComponent<Porta>();
                    scriptComplemento.temchave = cel.chave;
                    scriptComplemento.idSala = cel.idSalaPai;
                    scriptComplemento.areaPai = cel.dungeonPai.areaPai;
                }
                else if (cel.ehComplementoPorta1 == false && cel.ehComplementoPorta2 == false)
                {
                    Instantiate(tilePsala, tileCoord, RotateAntiClockwiseTwice());
                }
            }
            if (subTipoCelula == "quinaDBp")
            {
                Instantiate(tilePsalaCorner, tileCoord, RotateClockwiseTwice());
            }
            if (subTipoCelula == "quinaEBp")
            {
                Instantiate(tilePsalaCorner, tileCoord, RotateAntiClockwise());
            }
            if (subTipoCelula == "quinaDCp")
            {                                
                Instantiate(tilePsalaCorner, tileCoord, RotateClockwise());
            }
            if (subTipoCelula == "quinaECp")
            {                
                Instantiate(tilePsalaCorner, tileCoord, Quaternion.identity);
            }
            //
            if (subTipoCelula == "innerDBp")
            {
                Instantiate(tilePsalaInner, tileCoord, RotateClockwiseTwice());
            }
            if (subTipoCelula == "innerEBp")
            {
                Instantiate(tilePsalaInner, tileCoord, RotateAntiClockwise());
            }
            if (subTipoCelula == "innerDCp")
            {
                Instantiate(tilePsalaInner, tileCoord, RotateClockwise());
            }
            if (subTipoCelula == "innerECp")
            {
                Instantiate(tilePsalaInner, tileCoord, Quaternion.identity);
            }
            if (subTipoCelula == "individualp")
            {
                Instantiate(tileChao, tileCoord, Quaternion.identity);
            }

        }
        if (tipoCelula == "porta")
        {

            if (subTipoCelula == "esquerdap")
            {
                instanciaPorta = Instantiate(tilePorta, tileCoord, RotateAntiClockwise());
                scriptPorta = instanciaPorta.GetComponent<Porta>();
                scriptPorta.temchave = cel.chave;
                scriptPorta.idSala = cel.idSalaPai;
                scriptPorta.areaPai = cel.dungeonPai.areaPai;
                scriptPorta.principal = true;

            }
            if (subTipoCelula == "direitap")
            {
                instanciaPorta = Instantiate(tilePorta, tileCoord, RotateClockwise());
                scriptPorta = instanciaPorta.GetComponent<Porta>();
                scriptPorta.temchave = cel.chave;
                scriptPorta.idSala = cel.idSalaPai;
                scriptPorta.areaPai = cel.dungeonPai.areaPai;
                scriptPorta.principal = true;

            }
            if (subTipoCelula == "cimap")
            {
                instanciaPorta = Instantiate(tilePorta, tileCoord, Quaternion.identity);
                scriptPorta = instanciaPorta.GetComponent<Porta>();
                scriptPorta.temchave = cel.chave;
                scriptPorta.idSala = cel.idSalaPai;
                scriptPorta.areaPai = cel.dungeonPai.areaPai;
                scriptPorta.principal = true;


            }
            if (subTipoCelula == "baixop")
            {
                instanciaPorta = Instantiate(tilePorta, tileCoord, RotateClockwiseTwice());
                scriptPorta = instanciaPorta.GetComponent<Porta>();
                scriptPorta.temchave = cel.chave;
                scriptPorta.idSala = cel.idSalaPai;
                scriptPorta.areaPai = cel.dungeonPai.areaPai;
                scriptPorta.principal = true;

            }
        }

        if (tipoCelula == "caminho")
        {
            Instantiate(tileCaminho, tileCoord, RotateClockwise());
        }
        if (tipoCelula == "entrada")
        {
            if (ehEntradaSecreta == true)
            {
                if (subTipoCelula == "esquerdae")
                {
                    instanciaEntrada = Instantiate(tileEntradaSecreta, tileCoord, RotateClockwise());
                    scriptEntrada = instanciaEntrada.GetComponent<Entrada>();
                    scriptEntrada.areaOrigem = cel.dungeonPai.areaPai.posicaoMatriz;
                    scriptEntrada.areaDestino = cel.dungeonPai.areaPai.entradasSecretas[0];
                }
                if (subTipoCelula == "direitae")
                {
                    instanciaEntrada = Instantiate(tileEntradaSecreta, tileCoord, RotateAntiClockwise());
                    scriptEntrada = instanciaEntrada.GetComponent<Entrada>();
                    scriptEntrada.areaOrigem = cel.dungeonPai.areaPai.posicaoMatriz;
                    scriptEntrada.areaDestino = cel.dungeonPai.areaPai.entradasSecretas[1];
                }
                if (subTipoCelula == "cimae")
                {
                    instanciaEntrada = Instantiate(tileEntradaSecreta, tileCoord, RotateClockwiseTwice());
                    scriptEntrada = instanciaEntrada.GetComponent<Entrada>();
                    scriptEntrada.areaOrigem = cel.dungeonPai.areaPai.posicaoMatriz;
                    scriptEntrada.areaDestino = cel.dungeonPai.areaPai.entradasSecretas[2];
                }
                if (subTipoCelula == "baixoe")
                {
                    instanciaEntrada = Instantiate(tileEntradaSecreta, tileCoord, Quaternion.identity);
                    scriptEntrada = instanciaEntrada.GetComponent<Entrada>();
                    scriptEntrada.areaOrigem = cel.dungeonPai.areaPai.posicaoMatriz;
                    scriptEntrada.areaDestino = cel.dungeonPai.areaPai.entradasSecretas[3];
                }
            }
            else
            {
                if (subTipoCelula == "esquerdae")
                {
                    instanciaEntrada = Instantiate(tileEntrada, tileCoord, RotateClockwise());
                    scriptEntrada = instanciaEntrada.GetComponent<Entrada>();
                    scriptEntrada.areaOrigem = cel.dungeonPai.areaPai.posicaoMatriz;
                    scriptEntrada.areaDestino = cel.dungeonPai.areaPai.entradas[0];
                }
                if (subTipoCelula == "direitae")
                {
                    instanciaEntrada = Instantiate(tileEntrada, tileCoord, RotateAntiClockwise());
                    scriptEntrada = instanciaEntrada.GetComponent<Entrada>();
                    scriptEntrada.areaOrigem = cel.dungeonPai.areaPai.posicaoMatriz;
                    scriptEntrada.areaDestino = cel.dungeonPai.areaPai.entradas[1];
                }
                if (subTipoCelula == "cimae")
                {
                    instanciaEntrada = Instantiate(tileEntrada, tileCoord, RotateClockwiseTwice());
                    scriptEntrada = instanciaEntrada.GetComponent<Entrada>();
                    scriptEntrada.areaOrigem = cel.dungeonPai.areaPai.posicaoMatriz;
                    scriptEntrada.areaDestino = cel.dungeonPai.areaPai.entradas[2];
                }
                if (subTipoCelula == "baixoe")
                {
                    instanciaEntrada = Instantiate(tileEntrada, tileCoord, Quaternion.identity);
                    scriptEntrada = instanciaEntrada.GetComponent<Entrada>();
                    scriptEntrada.areaOrigem = cel.dungeonPai.areaPai.posicaoMatriz;
                    scriptEntrada.areaDestino = cel.dungeonPai.areaPai.entradas[3];
                }
            }
        }
        //
        if (tipoCelula == "contornoDungeon")
        {
            if (subTipoCelula == "esquerda")
            {
                if (cel.ehComplementoPorta1 == true)
                {
                    Instantiate(tileComplementoEntrada2, tileCoord, RotateClockwise());
                }
                else if (cel.ehComplementoPorta2 == true)
                {
                    Instantiate(tileComplementoEntrada1, tileCoord, RotateClockwise());
                }
                else if (cel.ehComplementoPorta1 == false && cel.ehComplementoPorta2 == false)
                {
                    Instantiate(tileContorno, tileCoord, RotateClockwise());
                }
            }
            if (subTipoCelula == "direita")
            {
                if (cel.ehComplementoPorta1 == true)
                {
                    Instantiate(tileComplementoEntrada1, tileCoord, RotateAntiClockwise());
                }
                else if (cel.ehComplementoPorta2 == true)
                {
                    Instantiate(tileComplementoEntrada2, tileCoord, RotateAntiClockwise());
                }
                else if (cel.ehComplementoPorta1 == false && cel.ehComplementoPorta2 == false)
                {
                    Instantiate(tileContorno, tileCoord, RotateAntiClockwise());
                }
            }
            if (subTipoCelula == "cima")
            {
                if (cel.ehComplementoPorta1 == true)
                {
                    Instantiate(tileComplementoEntrada1, tileCoord, RotateClockwiseTwice());
                }
                else if (cel.ehComplementoPorta2 == true)
                {
                    Instantiate(tileComplementoEntrada2, tileCoord, RotateClockwiseTwice());
                }
                else if (cel.ehComplementoPorta1 == false && cel.ehComplementoPorta2 == false)
                {
                    Instantiate(tileContorno, tileCoord, RotateClockwiseTwice());
                }
            }
            if (subTipoCelula == "baixo")
            {
                if (cel.ehComplementoPorta1 == true)
                {
                    Instantiate(tileComplementoEntrada2, tileCoord, Quaternion.identity);
                }
                else if (cel.ehComplementoPorta2 == true)
                {
                    Instantiate(tileComplementoEntrada1, tileCoord, Quaternion.identity);
                }
                else if (cel.ehComplementoPorta1 == false && cel.ehComplementoPorta2 == false)
                {
                    Instantiate(tileContorno, tileCoord, Quaternion.identity);
                }
            }
            if (subTipoCelula == "quinaDB")
            {
                Instantiate(tileContornoCorner, tileCoord, Quaternion.identity);
            }
            if (subTipoCelula == "quinaEB")
            {
                Instantiate(tileContornoCorner, tileCoord, RotateClockwise());
            }
            if (subTipoCelula == "quinaDC")
            {
                
                Instantiate(tileContornoCorner, tileCoord, RotateAntiClockwise());
            }
            if (subTipoCelula == "quinaEC")
            {
                
                Instantiate(tileContornoCorner, tileCoord, RotateClockwiseTwice());
            }
        }
        if (spawnPlayer == true)
        {
            Instantiate(spawn, tileCoord, Quaternion.identity);
           
        }
        if(spawnBoss == true)
        {
            Instantiate(bossaltar, tileCoord, Quaternion.identity);
        }
        if (spawnsbBoss == true)
        {
            Instantiate(bossback, tileCoord, Quaternion.identity);
        }

    }

    public static Dungeon gerarDungeon(Area areaPai)
    {
        //Inicializando Dungeon
        Dungeon dungeonInstanciada = new Dungeon(areaPai);
        while (dungeonInstanciada.errorFiltro || dungeonInstanciada.errorCaminho)
        {
            print("novo ciclo:");
            print("criando dungeon");
            dungeonInstanciada = new Dungeon(areaPai);
            print("inserindo dungeon pai:");
            dungeonInstanciada.inserirDungeonPai(dungeonInstanciada);
            print("fazendo super salas:");
            dungeonInstanciada.superSalas(dungeonInstanciada);
            print("inserindo os caminhos");
            dungeonInstanciada.InserirCaminhos(dungeonInstanciada); //Os valores do debugador anti-crash, são fixos. Em caso de mudar a amplitude da dungeon, é necessario observar esses valores.
            print("debungando os caminhos...");
            dungeonInstanciada.DebugPosCaminho(dungeonInstanciada);
            print("reestabelecendo as coordenadas");
            Dungeon.ReestabelecerCoords(dungeonInstanciada); // Para recuperar as coordenadas perdidas.
            print("filtrando a dungeon...");
            dungeonInstanciada.errorFiltro = dungeonInstanciada.FiltroCaminho(dungeonInstanciada);
            print("inserindo subtipos");
            dungeonInstanciada.InserirSubtipos(dungeonInstanciada);
            dungeonInstanciada.IgualarSuperSala(dungeonInstanciada);
            print("fim do ciclo");
            //Fim da inicialização.
        }
        //fora do while: acabamentos finais, que dependem de uma dungeon perfeita.
        //se um dia o spawn der erro de loop, por não caber em um corredor ou em outra regiao, é só adicionar um segundo while com o primeiro while e as funcoes de spawn dentro, e criar um bool da mesma forma que foi feito com o errorcaminho e filtro.
        dungeonInstanciada.AdicionarPlayerSpawn(areaPai, dungeonInstanciada);
        dungeonInstanciada.AdicionarBossSpawn(areaPai, dungeonInstanciada);
        //Console.WriteLine("dungeonCriada");
        return dungeonInstanciada;
    }

    public static Quaternion RotateClockwise()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, -90);

        return rotation;
    }
    public static Quaternion RotateAntiClockwise()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 90);

        return rotation;
    }
    public static Quaternion RotateAntiClockwiseTwice()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 180);

        return rotation;
    }
    public static Quaternion RotateClockwiseTwice()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, -180);

        return rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        scriptAtributos = FindObjectOfType(typeof(Atributos)) as Atributos;
        Zona zonaCriada = new Zona();
        scriptAtributos.zonaAtual = zonaCriada;
        this.zonaCriada = zonaCriada;
        int bossi = 0, bossj = 0;

        for (int i = 0; i < Config.tamanhoMatrizZona.x; i++)
        {
            for (int j = 0; j < Config.tamanhoMatrizZona.y; j++)
            {
                if (zonaCriada.zona[i, j] != null)
                {
                    gerarGrafico(zonaCriada.zona[i, j], i, j);
                    SpawnDeRecompensasSalas(zonaCriada.zona[i, j], i, j);
                    SpawnDeRecompensas(zonaCriada.zona[i, j], i, j);
                    SpawnDeInimigos(zonaCriada.zona[i, j], i, j);
                    SpawnDeInimigosSalas(zonaCriada.zona[i, j], i, j);
                    bossi = i; bossj = j;

                    if (zonaCriada.zona[i, j].inicial == true)
                    {
                        
                        scriptAtributos.areaAtual = zonaCriada.zona[i, j];
                    }
                }
                
            }
        }
        gerarGrafico(zonaCriada.bossArea, bossi + 1, bossj + 1);
        //

    }

    // Update is called once per frame
    void Update()
    {


    }

    void SpawnDeInimigos(Area areaPai, int linha, int coluna)
    {
        Database scriptDatabase = GetComponent<Database>();
        System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
        int pontosGerais = 0;
        switch (areaPai.dificuldade)
        {
            case 0:
                pontosGerais = rnd.Next(Config.pontoMf.x, Config.pontoMf.y + 1);
                break;
            case 1:
                pontosGerais = rnd.Next(Config.pontoN.x, Config.pontoN.y + 1);
                break;
            case 2:
                pontosGerais = rnd.Next(Config.pontoMd.x, Config.pontoMd.y + 1);
                break;
            case 3:
                pontosGerais = rnd.Next(Config.pontoD.x, Config.pontoD.y + 1);
                break;
            case 4:
                pontosGerais = rnd.Next(Config.pontoMuD.x, Config.pontoMuD.y + 1);
                break;
            case 5:
                pontosGerais = rnd.Next(Config.pontoI.x, Config.pontoI.y + 1);
                break;

        }
        int contAntibug = 0;
        while (pontosGerais > 0)
        {
            
            rnd = new System.Random(Guid.NewGuid().GetHashCode());
            int faixaFase = rnd.Next(1, 100 + 1);
            int fase = 1;

            if (faixaFase >= 79 && faixaFase <= 94) // Anterior
            {
                rnd = new System.Random(Guid.NewGuid().GetHashCode());
                fase = rnd.Next(Config.faseInicial, Config.faseAtual + 1);
            }
            if (faixaFase >= 1 && faixaFase <= 79) // Atual
            {
                fase = Config.faseAtual;
            }
            if (faixaFase >= 94 && faixaFase <= 99) //Posterior
            {
                rnd = new System.Random(Guid.NewGuid().GetHashCode());
                fase = rnd.Next(Config.faseAtual, Config.faseFinal + 1);
            }
            if (faixaFase >= 100) //Sem Fase
            {
                fase = -1;
            }

            rnd = new System.Random(Guid.NewGuid().GetHashCode());
            int faixaRaridade = rnd.Next(1, 2000 + 1);//não considera o spawn de boss.
            int raridade = 0;
            if (faixaRaridade >= 1 && faixaRaridade <= 1200)//Comum
            {
                raridade = 0;
            }
            if (faixaRaridade >= 1200 && faixaRaridade <= 1850)//Incomum
            {
                raridade = 1;
            }
            if (faixaRaridade >= 1851 && faixaRaridade <= 1970)//Raro
            {
                raridade = 2;
            }
            if (faixaRaridade >= 1971 && faixaRaridade <= 1990)//Epico
            {
                raridade = 3;
            }
            if (faixaRaridade >= 1991 && faixaRaridade <= 1999)//Lendario
            {
                raridade = 4;
            }
            if (faixaRaridade >= 2000)//Mini-Boss
            {
                raridade = 5;
            }

            //metodo que escolhe um inimigo a partir da fase e da raridade e retorna.
            GameObject inimigoSpawnado = Database.BuscarInimigo(fase, raridade);
            


            Dungeon dungeonInstanciada = areaPai.dungeonInserida;
            int xrand = 0, yrand = 0;
            bool permit = false;
            Celula cel = new Celula(new Vetor2(0, 0), "");
            while (permit == false)
            {
                rnd = new System.Random(Guid.NewGuid().GetHashCode());
                xrand = rnd.Next(1, dungeonInstanciada.tamanhoDungeon.x - 1);
                rnd = new System.Random(Guid.NewGuid().GetHashCode());
                yrand = rnd.Next(1, dungeonInstanciada.tamanhoDungeon.y - 1);
                cel = dungeonInstanciada.dungeon[xrand, yrand];
                if (cel.spawn == true || cel.subSpawn == true || cel.spawnMonstro == true || cel.tipoCelula != "caminho" || Celula.ChecarTipoAreaAdjacente(cel, "entrada", 2) != 0 || cel.spawnItem == true)
                {
                    permit = false;
                    if (contAntibug >= 999999) { permit = true; print("usou antibug, spawn de monstro geral."); }
                    contAntibug++;//declarado no inicio do for da sala, serve para caso não tenha mais espaço na sala para spawnar monstros.
                }
                else
                {
                    permit = true;
                    print("pemitiu legitimamente, spawn de monstro geral.");
                }
            }

            tileCoord = new Vector3(linha * 2 * Config.tamanhoMaxDungeon.x * tileSize + cel.coordCelulaDun.x * tileSize, -1 * coluna * 2 * Config.tamanhoMaxDungeon.y * tileSize + cel.coordCelulaDun.y * -1 * tileSize, 0);
            Instantiate(inimigoSpawnado, tileCoord, Quaternion.identity);
            cel.spawnMonstro = true;
            pontosGerais = pontosGerais - inimigoSpawnado.GetComponent<Inimigo>().pontos;
        }
    }

    void SpawnDeInimigosSalas(Area areaPai, int linha, int coluna)
    {
        for (int i = 0; i < areaPai.dungeonInserida.numeroSalas; i++)
        {
            int contAntibug = 0;
            Database scriptDatabase = GetComponent<Database>();
            System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
            int pontosGerais = 0;
            switch (areaPai.dungeonInserida.salas[i].dificuldade)
            {
                case 0:
                    pontosGerais = rnd.Next(Config.pontoSMf.x, Config.pontoSMf.y + 1);
                    break;
                case 1:
                    pontosGerais = rnd.Next(Config.pontoSN.x, Config.pontoSN.y + 1);
                    break;
                case 2:
                    pontosGerais = rnd.Next(Config.pontoSMd.x, Config.pontoSMd.y + 1);
                    break;
                case 3:
                    pontosGerais = rnd.Next(Config.pontoSD.x, Config.pontoSD.y + 1);
                    break;
                case 4:
                    pontosGerais = rnd.Next(Config.pontoSMuD.x, Config.pontoSMuD.y + 1);
                    break;
                case 5:
                    pontosGerais = rnd.Next(Config.pontoSI.x, Config.pontoSI.y + 1);
                    break;

            }
            while (pontosGerais > 0)
            {

                rnd = new System.Random(Guid.NewGuid().GetHashCode());
                int faixaFase = rnd.Next(1, 100 + 1);
                int fase = 1;

                if (faixaFase >= 79 && faixaFase <= 94) // Anterior
                {
                    rnd = new System.Random(Guid.NewGuid().GetHashCode());
                    fase = rnd.Next(Config.faseInicial, Config.faseAtual + 1);
                }
                if (faixaFase >= 1 && faixaFase <= 79) // Atual
                {
                    fase = Config.faseAtual;
                }
                if (faixaFase >= 94 && faixaFase <= 99) //Posterior
                {
                    rnd = new System.Random(Guid.NewGuid().GetHashCode());
                    fase = rnd.Next(Config.faseAtual, Config.faseFinal + 1);
                }
                if (faixaFase >= 100) //Sem Fase
                {
                    fase = -1;
                }

                rnd = new System.Random(Guid.NewGuid().GetHashCode());
                int faixaRaridade = rnd.Next(1, 2000 + 1);//não considera o spawn de boss.
                int raridade = 0;
                if (faixaRaridade >= 1 && faixaRaridade <= 1200)//Comum
                {
                    raridade = 0;
                }
                if (faixaRaridade >= 1200 && faixaRaridade <= 1850)//Incomum
                {
                    raridade = 1;
                }
                if (faixaRaridade >= 1851 && faixaRaridade <= 1970)//Raro
                {
                    raridade = 2;
                }
                if (faixaRaridade >= 1971 && faixaRaridade <= 1990)//Epico
                {
                    raridade = 3;
                }
                if (faixaRaridade >= 1991 && faixaRaridade <= 1999)//Lendario
                {
                    raridade = 4;
                }
                if (faixaRaridade >= 2000)//Mini-Boss
                {
                    raridade = 5;
                }

                // metodo que escolhe um inimigo a partir da fase e da raridade e retorna.
                GameObject inimigoSpawnado = Database.BuscarInimigo(fase, raridade);
                


                Dungeon dungeonInstanciada = areaPai.dungeonInserida;
                int xrand = 0, yrand = 0;
                bool permit = false;
                Celula cel = new Celula(new Vetor2(0, 0), "");
                while (permit == false)
                {
                    rnd = new System.Random(Guid.NewGuid().GetHashCode());
                    xrand = rnd.Next(1, dungeonInstanciada.salas[i].tamanhoSala.x - 1);
                    rnd = new System.Random(Guid.NewGuid().GetHashCode());
                    yrand = rnd.Next(1, dungeonInstanciada.salas[i].tamanhoSala.y - 1);
                    cel = dungeonInstanciada.salas[i].sala[xrand, yrand];
                    if (cel.spawn == true || cel.subSpawn == true || cel.spawnMonstro == true || cel.tipoCelula != "chao" || cel.spawnItem == true)
                    {
                        permit = false;
                        if (contAntibug >= 999999) { permit = true; print("usou anti-bug, spawn de monstro sala."); }
                        contAntibug++;//declarado no inicio do for da sala, serve para caso não tenha mais espaço na sala para spawnar monstros.
                    }
                    else
                    {
                        permit = true;
                        print("pemitiu legitimamente, spawn de monstro sala.");
                    }
                }

                tileCoord = new Vector3(linha * 2 * Config.tamanhoMaxDungeon.x * tileSize + cel.coordCelulaDun.x * tileSize, -1 * coluna * 2 * Config.tamanhoMaxDungeon.y * tileSize + cel.coordCelulaDun.y * -1 * tileSize, 0);
                Instantiate(inimigoSpawnado, tileCoord, Quaternion.identity);
                cel.spawnMonstro = true;
                pontosGerais = pontosGerais - inimigoSpawnado.GetComponent<Inimigo>().pontos;
            }
        }
    }

    void SpawnDeRecompensasSalas(Area areaPai, int linha, int coluna)
    {
        for (int i = 0; i < areaPai.dungeonInserida.numeroSalas; i++)
        {
            int contAntibug = 0;
            Database scriptDatabase = GetComponent<Database>();
            System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());

            rnd = new System.Random(Guid.NewGuid().GetHashCode());
            int faixaTipo = rnd.Next(1, 100 + 1);
            int tipo = 1;
            //chances de tipo, a depender da area e da sala:
            if (areaPai.secreta == true)//secreta com chave
            {
                if (areaPai.dungeonInserida.salas[i].chave == true)
                {
                    if (faixaTipo >= 79 && faixaTipo <= 93) // Bau
                    {
                        tipo = 2;
                    }
                    if (faixaTipo >= 1 && faixaTipo <= 79) // NPC
                    {
                        tipo = 3;
                    }
                    if (faixaTipo >= 94 && faixaTipo <= 99) //Bau
                    {
                        tipo = 2;
                    }
                    if (faixaTipo >= 100) //NPC
                    {
                        tipo = 3;
                    }

                }
                else //secreta sem chave
                {
                    if (faixaTipo >= 79 && faixaTipo <= 93) // Consumivel
                    {
                        tipo = 0;
                    }
                    if (faixaTipo >= 1 && faixaTipo <= 79) // Bau
                    {
                        tipo = 2;
                    }
                    if (faixaTipo >= 94 && faixaTipo <= 99) //NPC
                    {
                        tipo = 2;
                    }
                    if (faixaTipo >= 100) //Nada
                    {
                        continue;
                    }

                }
            }
            else
            {
                if (areaPai.dungeonInserida.salas[i].chave == true) //não secreta, com chave
                {
                    if (faixaTipo >= 79 && faixaTipo <= 93) // Consumivel
                    {
                        tipo = 0;
                        
                    }
                    if (faixaTipo >= 1 && faixaTipo <= 79) // Bau
                    {
                        tipo = 2;
                    }
                    if (faixaTipo >= 94 && faixaTipo <= 96) //Nada
                    {
                        continue;
                    }
                    if (faixaTipo >= 97) //NPC
                    {
                        tipo = 3;
                    }

                }
                else // não secreta, sem chave
                {
                    if (faixaTipo >= 79 && faixaTipo <= 93) // Bau
                    {
                        tipo = 2;
                    }
                    if (faixaTipo >= 1 && faixaTipo <= 79) // Consumivel
                    {
                        tipo = 0;
                    }
                    if (faixaTipo >= 94 && faixaTipo <= 99) //Nada
                    {
                        continue;
                    }
                    if (faixaTipo >= 100) //NPC
                    {
                        tipo = 3;
                    }

                }
            }
            //
            

            rnd = new System.Random(Guid.NewGuid().GetHashCode());
            int faixaRaridade = rnd.Next(1, 2000 + 1);//não considera o spawn de boss.
            int raridade = 0;
            if (faixaRaridade >= 1 && faixaRaridade <= 1200)//Comum
            {
                raridade = 0;
            }
            if (faixaRaridade >= 1200 && faixaRaridade <= 1850)//Incomum
            {
                raridade = 1;
            }
            if (faixaRaridade >= 1851 && faixaRaridade <= 1970)//Raro
            {
                raridade = 2;
            }
            if (faixaRaridade >= 1971 && faixaRaridade <= 1990)//Epico
            {
                raridade = 3;
            }
            if (faixaRaridade >= 1991 && faixaRaridade <= 1999)//Lendario
            {
                raridade = 4;
            }
            if (faixaRaridade >= 2000)//Lendario
            {
                raridade = 4;
            }

            // metodo que escolhe um inimigo a partir da fase e da raridade e retorna.
            GameObject itemSpawnado = Database.BuscarItem(tipo, raridade);



            Dungeon dungeonInstanciada = areaPai.dungeonInserida;
            int xrand = 0, yrand = 0;
            bool permit = false;
            Celula cel = new Celula(new Vetor2(0, 0), "");
            while (permit == false)
            {
                rnd = new System.Random(Guid.NewGuid().GetHashCode());
                xrand = rnd.Next(1, dungeonInstanciada.salas[i].tamanhoSala.x - 1);
                rnd = new System.Random(Guid.NewGuid().GetHashCode());
                yrand = rnd.Next(1, dungeonInstanciada.salas[i].tamanhoSala.y - 1);
                cel = dungeonInstanciada.salas[i].sala[xrand, yrand];
                if (cel.tipoCelula != "chao" || Celula.ChecarTipoAreaAdjacente(cel, "pSala", 1) != 3 || cel.spawnItem == true)
                {
                    permit = false;
                    if (contAntibug >= 999999) { permit = true; print("usou antibug, recompensa de sala."); }
                    contAntibug++;//declarado no inicio do for da sala, serve para caso não tenha mais espaço na sala para spawnar monstros.
                }
                else
                {
                    permit = true;
                    print("permitiu legitimamente, recompensa de sala");
                }
            }

            tileCoord = new Vector3(linha * 2 * Config.tamanhoMaxDungeon.x * tileSize + cel.coordCelulaDun.x * tileSize, -1 * coluna * 2 * Config.tamanhoMaxDungeon.y * tileSize + cel.coordCelulaDun.y * -1 * tileSize, 0);
            Instantiate(itemSpawnado, tileCoord, Quaternion.identity);
            cel.spawnItem = true;
        }
    }

    void SpawnDeRecompensas(Area areaPai, int linha, int coluna)
    {
        Database scriptDatabase = GetComponent<Database>();
        System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
        int tentativasDeSpawn = 0;
        tentativasDeSpawn = rnd.Next(0, Config.tentativaSpawnItemMAX + 1);

        int contAntibug = 0;
        for (int i = 0; i < tentativasDeSpawn; i++)
        {

            rnd = new System.Random(Guid.NewGuid().GetHashCode());
            int faixaTipo = rnd.Next(1, 1000 + 1);
            int tipo = 1;
            //chances de tipo, a depender da area e da sala:
            if (areaPai.secreta == true)//secreta
            {
                if (faixaTipo >= 1 && faixaTipo <= 750) // Nada
                {
                    continue;
                }
                if (faixaTipo >= 751 && faixaTipo <= 850) // Bau
                {
                    tipo = 2;
                }
                if (faixaTipo >= 851 && faixaTipo <= 950) //Consumivel
                {
                    tipo = 0;
                }
                if (faixaTipo >= 951) //NPC
                {
                    tipo = 3;
                }
            }
            else
            {

                if (faixaTipo >= 1 && faixaTipo <= 950) // Nada
                {
                    continue;
                }
                if (faixaTipo >= 953 && faixaTipo <= 1000) //Consumivel
                {
                    tipo = 0;
                }
                if (faixaTipo == 951) //NPC
                {
                    tipo = 3;
                }
                if (faixaTipo == 952) //Equipamento
                {
                    tipo = 1;
                }

            }


            rnd = new System.Random(Guid.NewGuid().GetHashCode());
            int faixaRaridade = rnd.Next(1, 2000 + 1);//não considera o spawn de boss.
            int raridade = 0;
            if (faixaRaridade >= 1 && faixaRaridade <= 1200)//Comum
            {
                raridade = 0;
            }
            if (faixaRaridade >= 1200 && faixaRaridade <= 1850)//Incomum
            {
                raridade = 1;
            }
            if (faixaRaridade >= 1851 && faixaRaridade <= 1970)//Raro
            {
                raridade = 2;
            }
            if (faixaRaridade >= 1971 && faixaRaridade <= 1990)//Epico
            {
                raridade = 3;
            }
            if (faixaRaridade >= 1991 && faixaRaridade <= 1999)//Lendario
            {
                raridade = 4;
            }
            if (faixaRaridade >= 2000)//Mini-Boss
            {
                raridade = 5;
            }

            //metodo que escolhe um inimigo a partir da fase e da raridade e retorna.
            GameObject itemSpawnado = Database.BuscarItem(tipo, raridade);



            Dungeon dungeonInstanciada = areaPai.dungeonInserida;
            int xrand = 0, yrand = 0;
            bool permit = false;
            Celula cel = new Celula(new Vetor2(0, 0), "");
            while (permit == false)
            {
                rnd = new System.Random(Guid.NewGuid().GetHashCode());
                xrand = rnd.Next(1, dungeonInstanciada.tamanhoDungeon.x - 1);
                rnd = new System.Random(Guid.NewGuid().GetHashCode());
                yrand = rnd.Next(1, dungeonInstanciada.tamanhoDungeon.y - 1);
                cel = dungeonInstanciada.dungeon[xrand, yrand];
                if (cel.spawn == true || cel.subSpawn == true || cel.spawnItem == true || (cel.tipoCelula != "caminho" && cel.tipoCelula != "chao") || Celula.ChecarTipoAreaAdjacente(cel, "entrada", 2) != 0 || Celula.ChecarTipoAreaAdjacente(cel, "porta", 1) != 0)
                {
                    permit = false;
                    if (contAntibug >= 999999) { permit = true; print("usou antibug, recompensa geral."); }
                    contAntibug++;//declarado no inicio do for da sala, serve para caso não tenha mais espaço na sala para spawnar monstros.
                }
                else
                {
                    permit = true;
                    print("permitiu legitimamente, recompensa geral");
                }
            }

            tileCoord = new Vector3(linha * 2 * Config.tamanhoMaxDungeon.x * tileSize + cel.coordCelulaDun.x * tileSize, -1 * coluna * 2 * Config.tamanhoMaxDungeon.y * tileSize + cel.coordCelulaDun.y * -1 * tileSize, 0);
            Instantiate(itemSpawnado, tileCoord, Quaternion.identity);
            cel.spawnItem = true;
        }
    }

}
