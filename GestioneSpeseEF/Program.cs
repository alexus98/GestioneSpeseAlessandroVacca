char scelta;

do
{
    Console.WriteLine($"============= Menu =============" +
        "\n[ 1 ] - Inserisci una nuova spesa" +
        "\n[ 2 ] - Approva una spesa" +
        "\n[ 3 ] - Cancella una spesa" +
        "\n[ 4 ] - Mostrare le spese approvate" +
        "\n[ 5 ] - Mostrare le spese di un utente" +
        "\n[ 6 ] - Mostrare il totale delle spese per categoria" +
        "\n[ Q ] - QUIT");

    scelta = Console.ReadKey().KeyChar;
    Console.WriteLine();

    switch (scelta)
    {
        case '1':
            ;
            break;
        case '2':
            ;
            break;
        case '3':
            ;
            break;
        case '4':
            ;
            break;
        case '5':
            ;
            break;
        case '6':
            ;
            break;
        case 'Q':
            Console.WriteLine("Arrivederci");
            break;
        default:
            Console.WriteLine("Scelta non valida");
            break;
    }
} while (scelta != 'Q');

