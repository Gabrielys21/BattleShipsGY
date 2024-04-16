static void Main()
{
    try
    {
        Console.WriteLine("Bienvenido al juego de Batalla Naval");

        const int TamañoTablero = 10;
        char[,] tableroJugador1 = new char[TamañoTablero, TamañoTablero];
        char[,] tableroJugador2 = new char[TamañoTablero, TamañoTablero];

        InicializarTableros(tableroJugador1, tableroJugador2);
        ColocarBarcos(tableroJugador1, "Jugador 1");
        ColocarBarcos(tableroJugador2, "Jugador 2");

        while (!JuegoTerminado(tableroJugador1, tableroJugador2))
        {
            Console.WriteLine("Tablero del Jugador 1:");
            MostrarTablero(tableroJugador1, true);
            Console.WriteLine("Tablero del Jugador 2:");
            MostrarTablero(tableroJugador2, false);

            RealizarTurno(tableroJugador1, tableroJugador2, "Jugador 1");
            if (JuegoTerminado(tableroJugador1, tableroJugador2))
                break;

            Console.WriteLine("Tablero del Jugador 1:");
            MostrarTablero(tableroJugador1, false);
            Console.WriteLine("Tablero del Jugador 2:");
            MostrarTablero(tableroJugador2, true);

            RealizarTurno(tableroJugador2, tableroJugador1, "Jugador 2");
        }
        Console.WriteLine("¡Juego terminado!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Se ha producido un error {ex.Message}");
    }
}

static void MostrarTablero(char[,] tablero, bool CensurarTabla)
{
    Console.WriteLine("  0 1 2 3 4 5 6 7 8 9");
    for (int i = 0; i < tablero.GetLength(0); i++)
    {
        Console.Write(i + " ");
        for (int j = 0; j < tablero.GetLength(1); j++)
        {
            if (CensurarTabla || tablero[i,j] != '-' && tablero[i,j] != 'A' && tablero[i,j] != 'P' && tablero[i,j] != 'F')
            {
                Console.Write(tablero[i, j] + " ");
            }
            else
            {
                Console.Write("X ");
            }
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

static void InicializarTableros(char[,] tableroJugador1, char[,] tableroJugador2)
{
    for (int i = 0; i < tableroJugador1.GetLength(0); i++)
    {
        for (int j = 0; j < tableroJugador1.GetLength(1); j++)
        {
            tableroJugador1[i, j] = '-';
            tableroJugador2[i, j] = '-';
        }
    }
}

static void ColocarBarcos(char[,] tablero, string jugador)
{
    Console.WriteLine($"Coloca tus barcos en el tablero del {jugador}:");
    int portaaviones = 3;  
    int fragata = 2;       
    int avion = 4;          

    
    while (true)
    {
        Console.WriteLine("Ingresa las coordenadas de inicio del portaaviones (fila y columna):");
        int filaPortaA = Convert.ToInt32(Console.ReadLine());
        int colPortaA = Convert.ToInt32(Console.ReadLine());

        if (EsPosibleColocarBarco(tablero, filaPortaA, colPortaA, portaaviones))
        {
            for (int i = 0; i < portaaviones; i++)
            {
                tablero[filaPortaA, colPortaA + i] = 'P';
            }
            break;
        }
        else
        {
            Console.WriteLine($"No es posible colocar el portaaviones en esa posición para el {jugador}. Por favor, intenta con otras coordenadas.");
        }
    }

    
    while (true)
    {
        Console.WriteLine("Ingresa las coordenadas de inicio de la fragata (fila y columna):");
        int filaFragata = Convert.ToInt32(Console.ReadLine());
        int colFragata = Convert.ToInt32(Console.ReadLine());

        if (EsPosibleColocarBarco(tablero, filaFragata, colFragata, fragata))
        {
            for (int i = 0; i < fragata; i++)
            {
                tablero[filaFragata, colFragata + i] = 'F';
            }
            break;
        }
        else
        {
            Console.WriteLine($"No es posible colocar la fragata en esa posición para el {jugador}. Por favor, intenta con otras coordenadas.");
        }
    }

    
    while (true)
    {
        Console.WriteLine("Ingresa las coordenadas de inicio del avión (fila y columna):");
        int filaAvion = Convert.ToInt32(Console.ReadLine());
        int colAvion = Convert.ToInt32(Console.ReadLine());

        if (EsPosibleColocarBarco(tablero, filaAvion, colAvion, avion))
        {
            for (int i = 0; i < avion; i++)
            {
                tablero[filaAvion, colAvion + i] = 'A';
            }
            break;
        }
        else
        {
            Console.WriteLine($"No es posible colocar el avión en esa posición para el {jugador}. Por favor, intenta con otras coordenadas.");
        }
    }
}

static bool EsPosibleColocarBarco(char[,] tablero, int filaInicio, int columnaInicio, int tamañoBarco)
{
    
    if (columnaInicio + tamañoBarco > tablero.GetLength(1))
    {
        return false;
    }

    
    for (int i = 0; i < tamañoBarco; i++)
    {
        if (tablero[filaInicio, columnaInicio + i] != '-')
        {
            return false;
        }
    }

    return true;
}

static void RealizarTurno(char[,] tableroAtacante, char[,] tableroDefensor, string jugador)
{
    Console.WriteLine($"Turno de ataque de {jugador}:");

    
    Console.WriteLine("Ingresa las coordenadas de tu disparo (fila y columna):");
    int fila = Convert.ToInt32(Console.ReadLine());
    int columna = Convert.ToInt32(Console.ReadLine());

    
    if (tableroDefensor[fila, columna] != '-')
    {
        Console.WriteLine($"¡Acertaste un {tableroDefensor[fila, columna]}!");
        tableroDefensor[fila, columna] = 'S';  
    }
    else
    {
        Console.WriteLine("Fallaste.");
        tableroDefensor[fila, columna] = 'O';  
    }
}

static bool JuegoTerminado(char[,] tableroJugador1, char[,] tableroJugador2)
{
    
    return TodosLosBarcosHundidos(tableroJugador1) || TodosLosBarcosHundidos(tableroJugador2);
}

static bool TodosLosBarcosHundidos(char[,] tablero)
{
    
    for (int i = 0; i < tablero.GetLength(0); i++)
    {
        for (int j = 0; j < tablero.GetLength(1); j++)
        {
            if (tablero[i, j] != '-' && tablero[i, j] != 'S' && tablero[i, j] != 'O')
            {
                return false;
            }
        }
    }

    return true;
}

static void CensurarTableroAtaque(char[,] Tablero, bool TurnoJugador)
{
    Console.WriteLine(" 0, 1, 2, 3, 4, 5, 6, 7, 8, 9");
    for (int i = 0; i < Tablero.GetLength(0); i++ )
    {
        Console.Write(i + " ");
        for (int j = 0; j < Tablero.GetLength(1); j++)
        {
            if (TurnoJugador || Tablero[i,j] == '-' || Tablero[i,j] == 'X' || Tablero[i,j] == 'O')
            {
                Console.WriteLine(Tablero[i,j] + " ");
            }
            else
            {
                Console.WriteLine("- ");
            }
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

Main();