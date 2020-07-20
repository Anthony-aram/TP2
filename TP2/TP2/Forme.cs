using System;

namespace TpHeritage
{
    public abstract class Forme
    {
        public abstract double Aire { get; }
        public abstract double Perimetre { get; }
        public override string ToString()
        {
            //return "Aire = " + Aire + Environment.NewLine + "Périmètre = " + Perimetre + Environment.NewLine;
            return $"Aire = {Aire}" + Environment.NewLine + $"Périmètre = {Perimetre}" + Environment.NewLine;
        }
    }
}