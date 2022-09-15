namespace Calculator.FigureGeometriche;

public class Rettangolo : IFiguraGeometrica {
    public double Larghezza { get; }

    public double Altezza { get; }

    public Rettangolo(double larghezza, double altezza) {
        Larghezza = larghezza;
        Altezza = altezza;
    }

    public double GetArea() {
        return Larghezza * Altezza;
    }

    public double GetPerimetro() {
        return (Larghezza + Altezza) * 2;
    }
}
