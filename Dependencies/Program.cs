
// === DEPENDENCIES ===
// Copiare le classi dell'esercizio precedente "Calculator" in un
// nuovo progetto di tipo "libreria di classi" in questa solution.
//
// Scrivere un programma che legge il file YAML "data.yml" ed utilizza
// le classi di "Calculator" per eseguire l'operazione indicata nel file.
//
// Note:
// - Il nome del file da leggere deve essere passato come parametro da linea di comando
// - Usare il pacchetto NuGet "YamlDotNet" per leggere i file YAML
// - Gestire errori di configurazione (figura non valida, operazione non valida)
// ====================

#region Esercizio bonus

// === DEPENDENCIES BONUS ===
// Se il programma rileva che il file ha estensione ".toml", deve interpretarlo
// come file TOML piuttosto che YAML.
//
// Note:
// - Usare il pacchetto NuGet "Tomlyn" per leggere i file TOML
// ==========================

#endregion

using Calculator.FigureGeometriche;
using Tomlyn;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

if (args.Length < 1) {
    // per i programmi non interattivi, la convenzione è di usare "Console" per l'output e "Console.Error" per i messaggi diagnostici
    // vedere post su StackOverflow https://stackoverflow.com/a/3385261
    Console.Error.WriteLine("Indicare il nome di un file.");
    Environment.Exit(1);
}

string fileName = args[0];
string fileContent = File.ReadAllText(fileName);

Dati dati;
if (Path.GetExtension(fileName) == ".toml") {
    dati = Toml.ToModel<Dati>(fileContent);
} else {
    IDeserializer yamlDeserializer = new DeserializerBuilder()
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .Build();

    dati = yamlDeserializer.Deserialize<Dati>(fileContent);
}

// chiede le dimensioni
IFiguraGeometrica figura;
switch (dati.Figura) {
    case "cerchio":
        if (dati.DimensioniCerchio == null) {
            Console.Error.WriteLine("Dimensioni cerchio non indicate.");
            Environment.Exit(2);
        }

        figura = new Cerchio(dati.DimensioniCerchio.Raggio);
        break;

    case "rettangolo":
        if (dati.DimensioniRettangolo == null) {
            Console.Error.WriteLine("Dimensioni rettangolo non indicate.");
            Environment.Exit(2);
        }

        figura = new Rettangolo(dati.DimensioniRettangolo.Larghezza, dati.DimensioniRettangolo.Altezza);
        break;

    case "quadrato":
        if (dati.DimensioniQuadrato == null) {
            Console.Error.WriteLine("Dimensioni quadrato non indicate.");
            Environment.Exit(2);
        }

        figura = new Quadrato(dati.DimensioniQuadrato.Lato);
        break;

    default:
        Console.Error.WriteLine("Figura non valida.");
        Environment.Exit(2);  // Exit() termina il programma
        figura = null;  // per zittire errore, in realtà non verrà mai eseguito
        break;
}

// calcola
double valore;
switch (dati.Calcola) {
    case "area":
        valore = figura.GetArea();
        break;

    case "perimetro":
        valore = figura.GetPerimetro();
        break;

    default:
        Console.Error.WriteLine("Operazione non valida.");
        Environment.Exit(2);
        valore = 0;  // per zittire errore, in realtà non verrà mai eseguito
        break;
}

// stampa il risultato
Console.WriteLine(valore);

class Dati {
    public string? Figura { get; set; }
    public string? Calcola { get; set; }
    public DimensioniCerchio? DimensioniCerchio { get; set; }
    public DimensioniRettangolo? DimensioniRettangolo { get; set; }
    public DimensioniQuadrato? DimensioniQuadrato { get; set; }
}

class DimensioniCerchio {
    public double Raggio { get; set; }
}

class DimensioniRettangolo {
    public double Larghezza { get; set; }
    public double Altezza { get; set; }
}

class DimensioniQuadrato {
    public double Lato { get; set; }
}
