using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public partial class CardArea : GComponent
    {
        public int turn = 0;

        public override void ConstructFromResource()
        {
            base.ConstructFromResource();
            btnLastTurn.onClick.Add(MinusTurn);
            btnNextTurn.onClick.Add(AddTurn);
        }

        public void UpdateView()
        {
            List<int> cardsIndex = NetManager.inst.seedDealer.cardsIndex;

            cardBack1.cardID.selectedIndex = cardsIndex[(turn + 1) * 3];
            cardBack2.cardID.selectedIndex = cardsIndex[(turn + 1) * 3 + 1];
            cardBack3.cardID.selectedIndex = cardsIndex[(turn + 1) * 3 + 2];

            cardFront1.Type.selectedIndex = (int)DeckInfo.Deck[cardsIndex[turn * 3]].CardType;
            cardFront2.Type.selectedIndex = (int)DeckInfo.Deck[cardsIndex[turn * 3 + 1]].CardType;
            cardFront3.Type.selectedIndex = (int)DeckInfo.Deck[cardsIndex[turn * 3 + 2]].CardType;
        }

        public void AddTurn() {
            turn++;
            UpdateView();
        }

        public void MinusTurn()
        {
            if (turn <= 0) return;
            turn--;
            UpdateView();
        }
    }
}