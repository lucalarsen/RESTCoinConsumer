using System;
using System.Collections.Generic;
using System.Text;

namespace RestCoinConsumer
{
    class Coin
    {

        public override string ToString()
        {
            return $"ID: {ID} Genstand: {Genstand} Bud: {Bud} Navn: {Navn}";
        }

        private static int nextid = 0;
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _genstand;

        public string Genstand
        {
            get { return _genstand; }
            set { _genstand = value; }
        }

        private int _bud;

        public int Bud
        {
            get { return _bud; }
            set { _bud = value; }
        }

        private string _navn;

        public string Navn
        {
            get { return _navn; }
            set { _navn = value; }
        }

        public Coin(string genstand, int bud, string navn)
        {
            Genstand = genstand;
            Bud = bud;
            Navn = navn;
            ID = nextid++;
        }

        public Coin()
        {
            ID = nextid++;
        }
    }
}
