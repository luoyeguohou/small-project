using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { 
    Astronaut = 0,
    Energy = 1,
    Plant = 2,
    Robot = 3,
    TimeTable = 4,
    Water = 5
}

public class Card { 
    public CardType CardType;
    public int number;
    public Card(CardType CardType,int number) { 
        this.CardType = CardType;
        this.number = number;
    }
}

public class SeedDealer
{
    public int[] Tasks;
    public List<int> cardsIndex = new List<int>();
    public void Deal(int seed)
    {
        Random.InitState(seed);
        // task
        Tasks = new int[3] { Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2) };
        // card
        for (int j = 0; j < 4; j++) { 
            int[] cards = new int[63];
            for (int i = 0; i < 63; i++) cards[i] = i;
            Shuffle(cards);
            foreach (var item in cards)  cardsIndex.Add(item);
        }
    }

    void Shuffle<T>(T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            // 生成一个 0 到 i 之间的随机索引
            int j = Random.Range(0, i + 1);

            // 交换元素
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}

public class DeckInfo
{
    public static readonly Card[] Deck = {
        new Card(CardType.Plant,9),
        new Card(CardType.Energy,9),
        new Card(CardType.Astronaut,10),
        new Card(CardType.Water,9),
        new Card(CardType.Robot,9),
        new Card(CardType.Robot,10),
        new Card(CardType.Plant,10),
        new Card(CardType.Energy,10),
        new Card(CardType.Energy,11),
        new Card(CardType.TimeTable,10),

        new Card(CardType.Water,10),
        new Card(CardType.Robot,11),
        new Card(CardType.Plant,11),
        new Card(CardType.Energy,11),
        new Card(CardType.Energy,12),
        new Card(CardType.Astronaut,12),
        new Card(CardType.Robot,11),
        new Card(CardType.Astronaut,13),
        new Card(CardType.TimeTable,12),
        new Card(CardType.Plant,12),

        new Card(CardType.Plant,14),
        new Card(CardType.TimeTable,13),
        new Card(CardType.Water,13),
        new Card(CardType.Robot,15),
        new Card(CardType.Energy,15),
        new Card(CardType.Robot,14),
        new Card(CardType.Plant,2),
        new Card(CardType.Robot,1),
        new Card(CardType.Energy,1),
        new Card(CardType.Water,3),

        new Card(CardType.Astronaut,3),
        new Card(CardType.Robot,2),
        new Card(CardType.Energy,4),
        new Card(CardType.Astronaut,4),
        new Card(CardType.TimeTable,3),
        new Card(CardType.Energy,5),
        new Card(CardType.TimeTable,4),
        new Card(CardType.Plant,4),
        new Card(CardType.Robot,5),
        new Card(CardType.Plant,5),

        new Card(CardType.Energy,5),
        new Card(CardType.Energy,6),
        new Card(CardType.Astronaut,6),
        new Card(CardType.Robot,5),
        new Card(CardType.Water,6),
        new Card(CardType.Robot,6),
        new Card(CardType.Plant,6),
        new Card(CardType.Energy,7),
        new Card(CardType.Energy,7),
        new Card(CardType.TimeTable,6),

        new Card(CardType.Robot,7),
        new Card(CardType.Plant,7),
        new Card(CardType.Plant,7),
        new Card(CardType.Plant,8),
        new Card(CardType.Astronaut,8),
        new Card(CardType.Water,7),
        new Card(CardType.Robot,8),
        new Card(CardType.Robot,8),
        new Card(CardType.Plant,8),
        new Card(CardType.Energy,9),

        new Card(CardType.TimeTable,8),
        new Card(CardType.Water,8),
        new Card(CardType.Plant,9),
    };
}