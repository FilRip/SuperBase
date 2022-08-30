using System;

namespace TestCallBaseBaseMethod
{
    class ClasseDeBase
    {
        public virtual string Ecrire(string param1)
        {
            Console.WriteLine("Classe de base (" + param1 + ")");
            return "retour ok";
        }
    }
    class PremiereClass : ClasseDeBase
    {
        public override string Ecrire(string param1)
        {
            Console.WriteLine("Classe intermédiaire");
            base.Ecrire(param1);
            return base.Ecrire(param1);
        }
    }
    class DerniereClasse : PremiereClass
    {
        public override string Ecrire(string param1)
        {
            Console.WriteLine("Classe finale");
            return this.SuperBase<ClasseDeBase, string>(param1);
        }
    }
    class Samples
    {
        static void Main(/*string[] args*/)
        {
            DerniereClasse maClasse = new();
            Console.WriteLine(maClasse.Ecrire("ok"));

            Console.ReadKey();
        }
    }
}
