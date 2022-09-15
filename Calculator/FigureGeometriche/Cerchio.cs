namespace Calculator.FigureGeometriche;

public class Cerchio : IFiguraGeometrica {
    // una property con getter implicito e senza setter può essere assegnata solo dal costruttore
    // in pratica è equivalente ad un campo readonly, assegnabile solo alla costruzione e non più modificabile
    public double Raggio { get; }

    public Cerchio(double raggio) {
        Raggio = raggio;
    }

    public double GetArea() {
        return Math.PI * Raggio * Raggio;
    }

    public double GetPerimetro() {
        return Math.PI * Raggio * 2;
    }
}
