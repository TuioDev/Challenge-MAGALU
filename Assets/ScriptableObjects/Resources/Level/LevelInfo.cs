using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level information", fileName = "Level_")]
public class LevelInfo : ScriptableObject
{
    [SerializeField] private PathCollection Paths;

    // Pegar um inimigo baseado no tempo da base
    // Tempo na fase: t1 = 0, tn = t;
    // Como saber qual inimigo pegar?
        // Um valor � atribuido para "Gastar"
        // Quanto maior o tempo, maior o valor total
        // Em cada fase d� pra definir um pre�o para os inimigos
        // O valor varia de acordo com o design para a quantidade final
    // A partir da quantidade de valor, � definido quais inimigo(s) spawnar
    // Executar um Coroutine para efetuar a apari��o dos inimigos na onda
        // O tempo entre cada apari��o: a1 = a; quanto tn tende ao infinito => an = 0;
    // Depois que todos os inimigos s�o spawnados?
        // Tempo de espera entre as ondas: e1 = e, quanto tn tende ao inifito => en = an * c;
}
