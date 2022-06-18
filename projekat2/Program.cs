using System;

namespace projekat2
{
    class Program
    {
         public struct Polje
        {
            public bool poseceno;
            public bool mina;
            public bool zastavica;
            public bool upitnik;
        }

        public static bool ponavljaj = true;

        static void Naslov()
        {
            Console.WriteLine(@"  __  __ _                                                   
 |  \/  (_)_ __   ___  _____      _____  ___ _ __   ___ _ __ 
 | |\/| | | '_ \ / _ \/ __\ \ /\ / / _ \/ _ \ '_ \ / _ \ '__|
 | |  | | | | | |  __/\__ \\ V  V /  __/  __/ |_) |  __/ |   
 |_|  |_|_|_| |_|\___||___/ \_/\_/ \___|\___| .__/ \___|_|   
                                            |_|              ");
        }

        public static string Uputstva = @" Zastavica [F]
 Upitnik [U]
 Otvaranje polja [Enter]";

        static void Pomoc()
        {
            Console.Clear();
            Console.WriteLine(@"Dobrodosli u Minesweeper.
Ako ste korisnik Windows-a, velike su sanse da vec imate ovu igru na svom racunaru. 
Ovaj vodic ce vam pomoci da završite svoju prvu igru.
Pravila igre su jednostavna, broj na polju pokazuje broj mina pored njega, a vas cilj je da otvorite sva polja bez mina.
");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("!");
            Console.ResetColor();
            Console.WriteLine(" - zastavica. Zastavicu stavite na mesto na kome ste sigurni da ima mina. [Z]");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("?");
            Console.ResetColor();
            Console.WriteLine(" - upitnik. Upitnik stavite na mesto na kome mislite da ima mina. [U]");
            Console.WriteLine();
            Console.WriteLine("[Esc] - povratak na meni iz igre.");
            Console.WriteLine();
            Console.WriteLine("Pritisnite [Esc] da biste se vratili u meni...");
            while (Console.ReadKey(true).Key != ConsoleKey.Escape)//program ceka da se pritisne escape
            { }
        }

        static void Izlaz_Iz_Programa()
        {
            Console.Clear();
            Console.WriteLine("Program uspešno napusten...");
            Environment.Exit(0);
        }

        static void Meni_Start()
        {
            string[] opcije = { "Igraj igru...", "Uputstva...", "Izlaz..." };
            int odabranaOpcija = Pokreni(opcije);

            //pocetne dimenzije(x i y osa) i broj mina
            int x_dimenzije = 0;
            int mine = 0;
            int y_dimenzije = 0;

            switch (odabranaOpcija)
            {
                case 0:
                    Console.Clear();
                    string[] opcije1 = { "Tabela 9 x 9", "Tabela 16 x 16", "Tabela 16 x 36", "Prilagodite...", "Nazad..." };
                    odabranaOpcija = Pokreni(opcije1);
                    switch (odabranaOpcija)
                    {
                        case 0:
                            x_dimenzije = y_dimenzije = 9;
                            mine = 10;
                            Console.Clear();
                            break;
                        case 1:
                            x_dimenzije = y_dimenzije = 16;
                            mine = 40;
                            Console.Clear();
                            break;
                        case 2:
                            x_dimenzije = 16;
                            y_dimenzije = 36;
                            mine = 99;
                            Console.Clear();
                            break;
                        case 3:
                            Console.Clear();
                            Naslov();
                            Console.WriteLine();
                            Console.WriteLine("Unesite dimenzije: ");
                            Console.Write("Visina [max 40]: ");
                            bool dobar_ulaz = false;
                            while (!dobar_ulaz)
                            {
                                dobar_ulaz = true;

                                while (!int.TryParse(Console.ReadLine(), out x_dimenzije))
                                {
                                    dobar_ulaz = false;
                                    Console.Write("Greska pri unosu. Duzina: ");
                                }
                                if (x_dimenzije > 40)
                                {
                                    dobar_ulaz = false;
                                    Console.Write("Smiri x koordinate. Duzina: ");
                                }
                            }
                            Console.Write("Duzina [max 40]: ");
                            dobar_ulaz = false;
                            while (!dobar_ulaz)
                            {
                                dobar_ulaz = true;

                                while (!int.TryParse(Console.ReadLine(), out y_dimenzije))
                                {
                                    dobar_ulaz = false;
                                    Console.Write("Greska pri unosu. Duzina: ");
                                }
                                if (y_dimenzije > 40)
                                {
                                    dobar_ulaz = false;
                                    Console.Write("Smiri y koordinate. Duzina: ");
                                }
                            }
                            Console.Write("Broj mina [max 250]: ");
                            dobar_ulaz = false;
                            while (!dobar_ulaz)
                            {
                                dobar_ulaz = true;
                                while (!int.TryParse(Console.ReadLine(), out mine))
                                {
                                    dobar_ulaz = false;
                                    Console.Write("Greska pri unosu. Broj mina: ");
                                }
                                if (mine >= x_dimenzije * y_dimenzije || mine > 250)
                                {
                                    dobar_ulaz = false;
                                    Console.Write("Greska pri unosu. Broj mina: ");
                                }
                                if (mine == 281105)
                                {
                                    Console.WriteLine(@"
...,,..*..*((####%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%####((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((########%%%%%%#########((((((((((///////////////**,,...              
      ...*..*,.*/(((####%%%%%%%%%%%%%%%%%%%%%%%%%%%%###(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((#####################(((((((((((////////*//////**..               
      ..,,..,..*(((((####%%%%%%%%%%%%%%%%%%%%%%%%%%####((((((((((((((((((((((((((((((((((((((((((((((((((((((/////////////////((((((((((#######%%##########(((((((((((/////********/****,.              
       .*,....,/(((((######%%%%%%%%%%%%%%%%%%%%%%%###(((((((((((((((((((((((((((((((((((((((((((((((((((////(/////////////////////(((((((###########(######(((((((((((((((///***********,,....          
        *,...,,/(((((#######%%%%%%%%%%%%%%%%%%%%%####((((((((((((((((((((((((((((((((((/((//////////((((/////////////////////////////((((###########((#####((((((((((((((((((//***///****,.             
.       .,..,,*/(((((########%%%%%%%%%%%%%%%%%%%#####((((((((((((((((((((((((((((((((((//////////////////////////////////////////////((((###########(((#####(((((((((((((((((((/*************,,,,..,,.. 
/        .... ,/(((###########%%%%%%%%%%%%%%%%%####(((((((((((((((((((((((((((((((//((///////////////////////////////////////////////((((((##########(((#####(((((((((((((((((((///**********,**,,,,****
              .*((((###########%%%%%%%%%%%%%%%###(((((((((((((((((((((///////////////////////////////////////////////////////////////((((((((##########(((#######((((((((((((((((////*******/***,.......
              .,/(((############%%%%%%%%%%%%%%##(((((((((((((((///////////////////////////////////////////////////////////////////////((((((((######%###((##########((#((((((((((((((((((/////******,...
.            .,,*((((############%%%%%%%%%%%%###(((((((((((((/////////////////////////////////////////////////////////////////////////(((((((((####%%%%%##(###########((((####((((((((((((////*********,
,            ,,,**/((((((#########%%%%%%%%%%%%##(((((((((((((///////////////////////////////////////////////////////////////((((((((((((((((((#######%%%%%%######################(((((((/////***,,......
.             ,,***//((((((#######%%%%%%%%%%%%##((((((((((((((///////////////////////////////////////////*****//////((((((##########################%%%%%%%%%%#####################(((((///*******,,....
.             ,,*****/((((((#######%%%%%%%%%%%##(((((((((((/((((((//////////////////////////////************/////((((#######%%%%%%%%%%%%%#%##########%%%%&&%%%%%%%#################(((((((//*******,,,..
.             ,,,,,,*///((((((######%%%%%%%%%%%#((((((((((((((((((((((//////////////////////////**********///(((((########%%%%%%%%%###############%%%%%%&&&&&&&&%%%%%%%%%%%%%%####((///((//////****,,...
.            .,,,,,,*////(((((#######%%%%%%%%%%##((((((((((((((((((#########(((((((//////////////***/****///(((((#######%%#####((((/////////((####%%%%%%%&&&&&&&&&&&%%%%%%%%%%%##((//**,/(/////****,,,,.
.            ...,,,*///////((((#######%%%%%%%%%##((((((((########%%%%%%%%#######((((((///////////****//////((((((((#######(((/////////////////(((((##%%%&&&&&&&&&&&&&&&&&%%%%%%#((((/*****//*/*****,,,,,
,...       .,,,,,,**////////((((######%%%%%%%%%%#((##########%%%%%%%%%%%%%%#########((((//((////****,,*///////((((((((((((((/////////////////////((((#%%&&&&&&&&&&&&&&&&%%%%%%%%##((//**//,**,*,,,,,,,,,
,,,,*,,,,,,,,,,,***///////(((((((#######%%%%%%%################(######%%%%#%########(((((((////******,,,*//////////////((((/////////((((//////////((((#%%%&&&&&&&&&&&%%%%%%%%%%%%##((/*****,,,,,,,,,.,,,
,  .,,,,**********//////////(((((######%%%%%%%%########(((((((((((((((((((###########(((((/////*****,,,,**//////////((((#####%%%%#%%####(((((((//////((%%%&&&&&&&%%%%%%&%%%%%%%%%##(((//*****,,,,,,,....
..    ..,,,,**////////(((((((((((#####%%%%%%%%%####((((((((/////////((((((((((((((###((((((////****,,,,,,*****//(((########%%%%&&&&%#((#%%%%###((/////(#%%&&&&&&%%%%%&&&&&%%%%%%%###(((//***/*******,,..
.*******,**///////(((((((((((((((#####%%%%%%%%%#(((((////////////((((##################((((////****,,,,,,,,***/((#####(((#%%%%%#%%%%%#//(##%%%##((////(#%%&&&&&&%%%%%&&&&&&%%%%%%##((((((///*********,,,
.*********//////(/((((((((((((((######%%%%%%%%%#(((//////((((###%%%%%%%%%#(((((#########((////*****,,,,,,,,,,**/(#########%%%%%%%%%%%######(((((((/////(%%%&&&&%%%%%&&&&&&&%%%%%####((((((////*******,.,
.****////////*/////(((((((((((#######%%%%%%%%%%#((/////(((#####(#%%%%%%%%%%#((((########(/////*****,,,,,,,,,,,**//(((#########((((((///////////////////(#%%&&&&%%%%%&&&&&&&&%%%%%%###((((((////*.,,,**..
 ,**,,,,,,******////(((((((#######%%%%%%%%%%%%%#((((//(#####((((#%%%%%#%%%%%###########(////******,,,,,,,,,,,,,,***//////(((((((////////////////////////(#%%%%%%%%%%&&&&&&&&%%%%%%%####((((////**,,,,,..
.,,,,************///(((((#########%%%%%%%%%%%%%%(((((((###########%%%%%%%%#########((((////*/*****,,,,,,,,,....,,,,****/////////////////************/////(%%%%%%%%%%%%&&&&&%%%%%%%%%#####((/////****,,..
.**,..,,**///////(((((((((###########%%%%%%%%%%%#(((((((#((((((((((((((((((((((((((((////*********,,,,,,,,......,,,,,,,*****//*/************************/(#%%%%%%%%%%%%&&&&%%%%%%%%%%##((((/////***,,,,.
,**,,,,**********/////(((((############%%%%%%%%%#(((((((((//////////(((((((((((((((((////*********,,,,,,,,,.........,,,,,,,,,*******************,,,,,***//(#%%%%%%%%%%%&&&&&%%#######(((///////***,,,,,,
,**,,,,,,*********////(((((############%%%%%%%%%#(((((((///////////(((((((((////////////////********,,,,,,,,,,,...,,,,,,,,,,,,,,,,,,,*******,,,,,,,,,,***//(%%%%%%%%%%%%%%%&%%########((//////***,,,,,.*
***,,,,,,*********////((((((############%%%%%%%%#(((((//////******///////////////////******/*********,,,,.........,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,**/(#%%%%%%%%%%%%%%%%%%%%###((((////**,....,,,**
,*,,,,,,,,***,,****//(((((((############%%%%%%%%#((((////////*****************,****////***************,,,.....  ....,,***,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,**/#%%%%%%%%%%%%%%%###%%#(((((((/////**/*******
.******,,,,,,,****//(((((/(((((##########%%%%%%%#((((////////*************,,,,,,**////*****************,,,,...     ..,,*/***,,,,,,,,,,,,,,,,,,,,,,,,,,.,,,*/(#%%%%%%%%%%%%%%%%%#####((/((/////*********,
 *****,,,,,,,****////////((((((###########%%%%%%#(((/////////***********,,,*,****///********************,,,,...      ..,*//****,,,,,,,,,,,,,,,,,,,,,,...,,**/#%%%%%%%%%%%#%%%%%%#(###((((////*,,,,**,,. 
 **,,*******,*****//////(((((((####((######%%%%#(((///////////*****************///************************,,,....    ..,,*///***,,,,,,,,,,,,,,,,,,,......,,**(#######%%#%%%###%%####((((((//***,****,,. 
 **,,,,,***********/*/////(((((((((((#######%%%##(////////////****************////*************************,,,,,........,,*////****,,,,,,,,,,,,,,,,,.....,,**/#######################((((((//*******,,  
 ************************///((((((((((###########(//////////****************/////***********///////************,,,,,,...,**///////***,,,,,,,,,,,,,,,,,...,,,**(######%%%#############((((////***,,**.   
 *********************,*/////////((((((##########(//////////****************/////****////////////////***************,,,,*****//////****,,,,,,,,,,,,,,,,,,,,,**/####%%%%%%%%#########((((/(///*****,...  
 **********,,,,,,,,,****////((((((((((((((#######(/////////****************///////****/////////////////**************,,,,,,*****/////***,,,,,,,,,,,,,,,,,,,,,*/#####%%%%%%#%########((///*///***,.      
 *********,,,,,,,,,****//////(((((((((((((#######(////*********************////////////////////////////*************,,,,,,,,,****/////****,,,,,,,,,,,,,,,,,,,*/#%%%%%%%####(((#####(((///****,,,..      
 *******,,,,,,,,,,*****//////////((((((((((######(/////******************//////////////////////////////************,,,,,,,,,,,,,**/////****,,,,,,,,,,,,,,,,,,**###%%%%%###########(##(((/**,,,,..       
 *******,,,,,,,,,,,******//***//////((((((((#####(/////******************///////////////////////////////////******,,,,,,,,,,,,,,,***////****,,,,,,,,,,,,,,,,***###%#%%#######(((#####((//*,*,*,.        
 ******,,,    ..,,,,,********//////////(((((####((//////****************/////////////////////////////////////****,,,,,,,,,,,,,,,,,***////****,,,,,,,,,,,,,,,,**(####################(((//*,,,*..   ..   
 ******,.      .,...,,,****..*///////////((((((((((//******************///////////////////////////////////////*/**,,,,,,,,.....,,,,**/////***,,,,,,,,,,,,,,***/##################(((((/////**,,.....    
 ******.       .,.. .,,***,,,***/////////((((((((((//*/****************//////////////////////////////////////*****,,,,,,.....  ..,,,**////****,,,,,,,,,,,,,***//(###############(((((/***/**,......     
 ****,        .... . ,*****,*.,,*****////(((((((((((*******************/////////////////////////////////////*******,,,,,......   ..,,**/////**,,,,,,,,,,,,***((((//**///(((###(((((//,.                 
 ***,             ...,,*******..*******///////////((/******************/////////*******////////////////////*///////*****,,,,...  ...,**/////**,,,,,,,,,,,,**/(#(//(/((//****/(((((*,.                   
 **,                .,*****,*,,.********//////****////*******/******///////////********//////////////////////////////////***,,......,***////***,,,,,,,,,,,**(####(///*/**/(((/*,*,.                     
 *.                  .,******/,.**********//***,,//////,,****//**/*////////////****//////////////(((((((((/////**//****//(((//**,,...,**////***,,,,,,,,,,,**(#####((/(//(/*.....,,,                     
                      ,,******,.**********,.. .,**////((*,********//*//////////****/////(((//////***,,*******,,**********////(((/**,.,***///***,,,,,,,,,,,*((((###((**/,,,,..,..,,,                     
                      .,*****,. ****/////*. ..,****////((/.,********///////////***///(((((//////***,,,,******,,,,*******/##((#%#((/*,,***///***,,,,,,,,,,,((#(((#((/*,**,.. .....                       
.                      ,**///*  ****///(((    .*****///(((/,,,*******//////////***/(((((##//**//*****************/(%%%%%%%%%%%%%##(/*,***///***,,,,,,,,,,/(###//((//**,,                                
(                      .,/////  *////((/*/.    *****/////(((,.,********/////////*//(((###%%%%%%%#(((((#####%%%%%%%%%%%%%%%%%%%%%##((/****////***,,,,,,,,*#####((/((/*,.,                               .
(                       ,*/*,*, /////(((((.    ,,***////(((((*.,*******////////////((###%%%%%%%%%%######################%%%%%%#####(//*/*/////**,,,,,,.,(######((((/...                                (
(                       .,**.** /////(((((,     ,****////(((((/,,********//////////((######%%%%%##########################%%%%######(/////////**,.,,...*#######(((//*.                               .((
(                        ,**,,/.//////((((*       .**///((((((((,,,******/////////((((######%%%############################%%%######(//////////*,.,,..,/#######(/,.                                  /((
(                        .**,***//////((((*          .***///(((((,.,,****///////////((#################((((((###############%#######(//////////*,,,,,.,/(#####(**,,.                                /(((
(                         ,**.*///////((((/      ,,,,.  .**//****,..,,,****////////(((#######%########((((((((###(((###############((/**///////*,.....,/#(/(//*****,                               /((##
(                         .**.,///////((((/       .,. ...,********....,,,**/////////(((###############(((((((((((((((##############((/***//////*,.....,,  ,****,,,,.                              /((##(
(                          ,*,,///////(((((       ,,,,,,,.,*******,...,,,,*///////////((#############((((((((((((((((((############((/***//////*,.......,,,******,.                              /((###(
(                          .**.,*/////(((((,.     .,,,.,,*..*******.....,,,*///////////((###########((((((((((((((((((((##########((/*,**//////*,..,,,,,********.                               /(((##((
(                           ,*,,//////(((((,,,.    .,,,,..*********,.....,,**///////////((((((#####(((((((((((((((((((((###(######(/*,,**//////*,,,,,,,,********.                              *((((((((
(                           .****//////((((*..,,,,,,,,,.,..,********. ....,,*////////////(#####((((((((((((((((((##########(((((#((/*,,**//////*,,,,,,,*******,.                              *(((((((((
(                           .**,,//////((((/*******,,,,,*,**********,.....,,*/////////////(((((((((((((((((#################((((#(/**,,**//////*,,,,,,,*******.                   ...,.      *((##((((((
/                            ,*,,//////(((((((((((((((#(((((((((((((/....,..,*////////**////(((((((###########%%%%%%%%%%%%#(((((((/**,,****///*,,,,,,,,*******.                  .,,..,     *((##(((((((");
                                    Environment.Exit(1);
                                }
                            }
                            Console.Clear();
                            break;

                        case 4:
                            Meni_Start();
                            break;
                    }
                    break;

                case 1:
                    Pomoc();
                    Meni_Start();
                    break;

                case 2:
                    Izlaz_Iz_Programa();
                    break;
                    
            }
            
            Polje[,] tabelica = new Polje[x_dimenzije, y_dimenzije];

            for (int i = 0; i < tabelica.GetLength(0); i++)
            {
                for (int j = 0; j < tabelica.GetLength(1); j++)
                {
                    tabelica[i, j].poseceno = tabelica[i, j].mina = false;
                }
            }

            tabelica = Upisi_minice(tabelica, mine, x_dimenzije, y_dimenzije);


            int[] odabrano_polje = { 0, 0 };
            Ispis(tabelica, odabrano_polje[0], odabrano_polje[1], mine);
            ponavljaj = true;

            while (ponavljaj)
            {
                odabrano_polje = BiranjePolja(tabelica, odabrano_polje[0], odabrano_polje[1], mine);
                if (tabelica[odabrano_polje[0], odabrano_polje[1]].mina)
                {
                    Console.Clear();
                    Ispis_Tabelice_Poraz(tabelica);
                }
                else
                {
                    Otvori_Polje(tabelica, odabrano_polje[0], odabrano_polje[1]);
                }
                if (ponavljaj == false)
                {
                    Meni_Start();
                }
            }
        }

        static int Pokreni(string[] Options)
        {
            int indeks = 0;

            ConsoleKey pritisnuto_dugme;
            do
            {
                Console.Clear();
                Displej(Options, indeks);

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    indeks++;
                }
                if (indeks == Options.Length)
                {
                    indeks = 0;
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    indeks--;
                }
                if (indeks == -1)
                {
                    indeks = Options.Length - 1;
                }
                pritisnuto_dugme = keyInfo.Key;
            } while (pritisnuto_dugme != ConsoleKey.Enter); //program ceka da se pritisne enter
            return indeks;
        }

        static void Displej(string[] Options, int indeks)
        {
            Naslov();
            Console.WriteLine();
            for (int i = 0; i < Options.Length; i++)
            {
                if (i == indeks)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("-> ");
                    string trenutna_opcija = Options[i];
                    Console.WriteLine(trenutna_opcija);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    string trenutna_opcija = Options[i];
                    Console.WriteLine("  " + trenutna_opcija);
                }
                Console.ResetColor();
            }
        }

        static void Surenje()
        {
            Console.WriteLine(@"  ___                              _                  _ 
 |_ _|__ _ _ __ __ _    __ _  ___ | |_ _____   ____ _| |
  | |/ _` | '__/ _` |  / _` |/ _ \| __/ _ \ \ / / _` | |
  | | (_| | | | (_| | | (_| | (_) | || (_) \ V / (_| |_|
 |___\__, |_|  \__,_|  \__, |\___/ \__\___/ \_/ \__,_(_)
     |___/             |___/                            
                                         
                                                  
Pritinsite [Enter] da nastavite...");
            ponavljaj = false;
        }

        //upis mina nasumicno u tabelu
        static Polje[,] Upisi_minice(Polje[,] tabelica, int mine, int x_dimenzije, int y_dimenzije)
        {
            Random rand_generator = new Random();
            for (int i = 0; i < mine; i++)
            {
                int minica_x = rand_generator.Next(0, x_dimenzije);
                int minica_y = rand_generator.Next(0, y_dimenzije);
                if (tabelica[minica_x, minica_y].mina)
                {
                    i--;
                }
                else tabelica[minica_x, minica_y].mina = true;
            }
            return tabelica;
        }
        //metoda za ispis tabele nakon 
        static void Ispis_Tabelice_Poraz(Polje[,] tabelica)
        {
            Naslov();
            Console.WriteLine(Uputstva);
            Console.WriteLine();
            for (int i = 0; i < tabelica.GetLength(0); i++)
            {
                Console.Write("  ");
                for (int j = 0; j < tabelica.GetLength(1); j++)
                {
                    if (tabelica[i, j].mina != true)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write(Izbroji_Susede(tabelica, i, j));
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("☼");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Pritisnite [Enter] da nastavite...");
            ponavljaj = false;
            while (Console.ReadKey(true).Key != ConsoleKey.Enter)
            { }
        }

        static int[] BiranjePolja(Polje[,] tabela, int trenutno_x, int trenutno_y, int mine)
        {
            int brojNeotvorenihPolja = 0;
            for (int j = 0; j < tabela.GetLength(0); j++)
            {
                for (int k = 0; k < tabela.GetLength(1); k++)
                {
                    if (!tabela[j, k].poseceno)
                    {
                        brojNeotvorenihPolja++;
                    }
                }
            }

            int[] x_i_y = new int[2];

            if (brojNeotvorenihPolja == mine)
            {
                Surenje();
            }

            ConsoleKeyInfo pritisnuto_dugme;
            do
            {
                pritisnuto_dugme = Console.ReadKey(true);

                if (pritisnuto_dugme.Key == ConsoleKey.UpArrow)
                {
                    if (trenutno_x == 0)
                    {
                        trenutno_x = tabela.GetLength(0) - 1;
                    }
                    else
                    {
                        trenutno_x--;
                    }
                    Console.Clear();
                    Ispis(tabela, trenutno_x, trenutno_y, mine);
                }
                else if (pritisnuto_dugme.Key == ConsoleKey.DownArrow)
                {
                    if (trenutno_x == tabela.GetLength(0) - 1)
                    {
                        trenutno_x = 0;
                    }
                    else
                    {
                        trenutno_x++;
                    }
                    Console.Clear();
                    Ispis(tabela, trenutno_x, trenutno_y, mine);
                }
                else if (pritisnuto_dugme.Key == ConsoleKey.RightArrow)
                {
                    if (trenutno_y == tabela.GetLength(1) - 1)
                    {
                        trenutno_y = 0;
                    }
                    else
                    {
                        trenutno_y++;
                    }
                    Console.Clear();
                    Ispis(tabela, trenutno_x, trenutno_y, mine);
                }
                else if (pritisnuto_dugme.Key == ConsoleKey.LeftArrow)
                {
                    if (trenutno_y == 0)
                    {
                        trenutno_y = tabela.GetLength(1) - 1;
                    }
                    else
                    {
                        trenutno_y--;
                    }
                    Console.Clear();
                    Ispis(tabela, trenutno_x, trenutno_y, mine);
                }
                else if (pritisnuto_dugme.Key == ConsoleKey.F)
                {
                    if (!tabela[trenutno_x, trenutno_y].upitnik && !tabela[trenutno_x, trenutno_y].zastavica && !tabela[trenutno_x, trenutno_y].poseceno)
                    {
                        tabela[trenutno_x, trenutno_y].zastavica = true;
                    }
                    else
                    {
                        tabela[trenutno_x, trenutno_y].zastavica = false;
                    }
                    Console.Clear();
                    Ispis(tabela, trenutno_x, trenutno_y, mine);
                }
                else if (pritisnuto_dugme.Key == ConsoleKey.U)
                {
                    if (!tabela[trenutno_x, trenutno_y].zastavica && !tabela[trenutno_x, trenutno_y].upitnik && !tabela[trenutno_x, trenutno_y].poseceno)
                    {
                        tabela[trenutno_x, trenutno_y].upitnik = true;
                    }
                    else
                    {
                        tabela[trenutno_x, trenutno_y].upitnik = false;
                    }
                    Console.Clear();
                    Ispis(tabela, trenutno_x, trenutno_y, mine);
                }
                else if (pritisnuto_dugme.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Ispis_Tabelice_Poraz(tabela);
                    break;
                }
            }
            while (pritisnuto_dugme.Key != ConsoleKey.Enter);
            x_i_y[0] = trenutno_x;
            x_i_y[1] = trenutno_y;
            return x_i_y;
        }
        //ispis tabele
        static void Ispis(Polje[,] tabela, int trenutni_x, int trenutni_y, int mine)
        {
            Naslov();
            Console.WriteLine(Uputstva);
            Console.WriteLine();
            for (int i = 0; i < tabela.GetLength(0); i++)
            {
                Console.Write("  ");
                for (int j = 0; j < tabela.GetLength(1); j++)
                {
                    if (tabela[i, j].zastavica)
                    {
                        if (i == trenutni_x && j == trenutni_y)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                        }
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("!");
                    }
                    else if (tabela[i, j].upitnik)
                    {
                        if (i == trenutni_x && j == trenutni_y)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                        }
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write("?");
                    }
                    else if (tabela[i, j].poseceno)
                    {
                        if (i == trenutni_x && j == trenutni_y)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                        }
                        int susedi = Izbroji_Susede(tabela, i, j);
                        if (susedi == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(susedi);
                        }
                        else if (susedi == 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(susedi);
                        }
                        else if (susedi == 3)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write(susedi);
                        }
                        else if (susedi == 4)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write(susedi);
                        }
                        else if (susedi == 5)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write(susedi);
                        }
                        else if (susedi == 6)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write(susedi);
                        }
                        else if (susedi == 7)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.Write(susedi);
                        }
                        else if (susedi == 8)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(susedi);
                        }
                        else if (susedi == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("-");
                        }
                        Console.ResetColor();
                    }
                    else
                    {
                        if (i == trenutni_x && j == trenutni_y)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                        }
                        Console.Write(" ");
                    }
                    Console.ResetColor();
                }

                int brZastavica = 0;

                for (int j = 0; j < tabela.GetLength(0); j++)
                {
                    for (int k = 0; k < tabela.GetLength(1); k++)
                    {
                        if (tabela[j, k].zastavica)
                        {
                            brZastavica++;
                        }
                    }
                }
                int maxm = mine;
                if (i == tabela.GetLength(0) - 1)
                {
                    Console.Write("\n\nPreostale zastave: ");
                    if (maxm - brZastavica < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    Console.WriteLine(maxm - brZastavica);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
        static int Izbroji_Susede(Polje[,] tabela, int x, int y)
        {
            int susedi = 0;
            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (Proveri_Dimenzije(i, j, tabela) && tabela[i, j].mina == true)
                    {
                        susedi++;
                    }
                }
            }
            if (susedi == 0)
            {
                Otvori_Susede(tabela, x, y);
            }
            return susedi;
        }
        //implementacija backtracking-a
        static void Otvori_Susede(Polje[,] tabela, int x, int y)
        {
            Otvori_Polje(tabela, x - 1, y - 1); Otvori_Polje(tabela, x - 1, y);
            Otvori_Polje(tabela, x - 1, y + 1); Otvori_Polje(tabela, x, y - 1);
            Otvori_Polje(tabela, x, y + 1); Otvori_Polje(tabela, x + 1, y - 1);
            Otvori_Polje(tabela, x + 1, y); Otvori_Polje(tabela, x + 1, y + 1);
        }

        static void Otvori_Polje(Polje[,] tabela, int x, int y)
        {
            if (Proveri_Dimenzije(x, y, tabela) && tabela[x, y].poseceno != true)
            {
                tabela[x, y].poseceno = true;
                if (Izbroji_Susede(tabela, x, y) == 0)
                    Otvori_Susede(tabela, x, y);
            }
            else return;
        }

        static bool Proveri_Dimenzije(int x, int y, Polje[,] tabela)
        {
            return x >= 0 && x < tabela.GetLength(0) && y >= 0 && y < tabela.GetLength(1);
        }
        
        static void Main(string[] args)
        {
            Meni_Start();
        }
    }
}
