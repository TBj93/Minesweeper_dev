wausing Minesweeper.Classes.Container;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
  public partial class Form1 : Form
  {
    private List<CclField> I_liFields;
    private List<CclField> I_liRND;
    private List<CclField> I_liNeighbours;
    private Random I_rndNumbers;

    public Form1()
    {
      InitializeComponent();

      I_liRND = new List<CclField>();
      I_liFields = new List<CclField>();
      I_rndNumbers = new Random();





      int iFieldSize = 30;


      CreateMineField(10, 10, iFieldSize);
      AddMines(10);




      GameStatus.Text = "In Progress";
      //   label1.Text = I_liFields.ToArray;




    }


    private void CreateMineField(int _iColCount, int _iRowCount, int _iFieldSize)
    {
      I_liFields.Clear();

      for (int iColIndex = 0; iColIndex < _iColCount; iColIndex++)
      {
        for (int iRowIndex = 0; iRowIndex < _iRowCount; iRowIndex++)
        {
          CclField clField = new CclField(this,
                                          iRowIndex * _iFieldSize,
                                          iColIndex * _iFieldSize,
                                          _iFieldSize,
                                          _iFieldSize);
          I_liFields.Add(clField);

          SetLastFieldsNeighbous( _iRowCount);
        }
      }
    }

    private void SetLastFieldsNeighbous( int _iRowCount)
    {
      CclField clField    = I_liFields.Last();
      int      iLastIndex = I_liFields.Count - 1;

      ConnectTopNeighbour(clField, iLastIndex, _iRowCount);
      ConnectTopLeftNeighbour(clField, iLastIndex, _iRowCount);
      ConnectLeftBottomNeighbour(clField, iLastIndex, _iRowCount);
      ConnectLeftNeighbour(clField, iLastIndex, _iRowCount);
    }

    private void ConnectTopNeighbour(CclField _clField, int _iFieldIndex, int _iRowCount)
    {
      bool bIsTopRow = (_iFieldIndex % _iRowCount) == 0;

      if (bIsTopRow)
        return;

      CclField clTop = I_liFields[_iFieldIndex-1];

      _clField.Neighbours.Add(clTop);
      clTop.Neighbours.Add(_clField);
    }


    private void ConnectTopLeftNeighbour(CclField _clField, int _iFieldIndex, int _iRowCount)
    {
      bool bIsTopRow = (_iFieldIndex % _iRowCount ) == 0;
      bool bIsLeft = (_iFieldIndex < _iRowCount);

      if (bIsTopRow || bIsLeft)
        return;

      CclField clTopLeft = I_liFields[_iFieldIndex - 1 - _iRowCount];

      _clField.Neighbours.Add(clTopLeft);
      clTopLeft.Neighbours.Add(_clField);
    }


    private void ConnectLeftNeighbour(CclField _clField, int _iFieldIndex, int _iRowCount)
    {
      bool bIsLeft = (_iFieldIndex < _iRowCount);

      if ( bIsLeft)
        return;

      CclField clLeft = I_liFields[_iFieldIndex - _iRowCount];

      _clField.Neighbours.Add(clLeft);
      clLeft.Neighbours.Add(_clField);
    }


    private void ConnectLeftBottomNeighbour(CclField _clField, int _iFieldIndex, int _iRowCount)
    {
      bool bIsBottomRow = (_iFieldIndex % _iRowCount) == _iRowCount-1;
      bool bIsLeft = (_iFieldIndex < _iRowCount);

      if (bIsBottomRow || bIsLeft)
        return;

      CclField clBottomLeft = I_liFields[_iFieldIndex + 1 - _iRowCount];

      _clField.Neighbours.Add(clBottomLeft);
      clBottomLeft.Neighbours.Add(_clField);
    }



    private void AddMines(int _iNumberOfMines)
    {

      for (int iCounter = 0; iCounter < _iNumberOfMines; iCounter++)
      {
        List<CclField> liOpenFields = I_liFields.FindAll(clEntry => !clEntry.HasMine);

        int iRND = I_rndNumbers.Next(liOpenFields.Count);



        liOpenFields[iRND].HasMine = true;

        // über linq und distance neighbours finden?
        // liOpenFields[iRND]


      }
    }






    //List<int> liIndizes = new List<int>();
    //
    //do
    //{
    //    int iRNDIndex = I_rndNumbers.Next(I_liFields.Count);
    //
    //    if (!liIndizes.Contains(iRNDIndex))
    //        liIndizes.Add(iRNDIndex);
    //
    //} while (liIndizes.Count < _iNumberOfMines);


    // find adjacent mines
    // minen mit koordianten in array speichern
    // bei aufdecken die koordinaten vergleichen
    /*
     * vergleichen wo liOpenfields[x].HasMine = true;
     * liste von fieldsbutton mit open vergleichen
     * wenn die stelle hasmine=true die stelle aufdecken
     * in dem fall show mit open.png
     * 
     * wie viele minen werden aufgedeckt? beim click
     * rekursiv, wird ein feld ohne nachbarminen frei deckt es sich auch auf, ansonsten zeigt es zahl
     * 
     * 
     * demfall geht er die liste durch
     * 
     * 
     * die koords sind mit op und left in der liste!!!
     * 
     * liflieds.find  where y,x koords in nähe usw
     * 
     * location of button?
     * 
     * 
     */

//        ##########          FIND NEIGHBOURS       #############################


    private void FindNeighbours(int x, int y) {

      List<CclField> neighbours = I_liFields.FindAll(field => field.FieldButton.Location.X >= (x - 1) && field.FieldButton.Location.X <= (x + 1)
                                  && field.FieldButton.Location.Y >= (y - 1) && field.FieldButton.Location.Y <= (y + 1));

    }


    /*
                for (int iCounter = 0; iCounter < _iNumberOfMines; iCounter++)
                {
                    List<CclField> liOpenFields = I_liFields.FindAll(clEntry => !clEntry.HasMine);

                    //   Koordinaten:     x/ int _iTop         y/ int _iLeft

                  //  IEnumerable<string> query = fruits.Where(fruit => fruit.Length < 6);


                }
    --------------------
    --------------------               V O R G E H E N  der Show Methode

                /* links klick auf button


                 * -->showMines methode ausführen
                 * ---> über liFields.findAll() gucken ob mine
                 * -----> falls keine mine, felder aufdecken (mit open.png)
                 * 
                 * listenpunkt: als bsp. index 17 (von0-99)
                 * von punkt 17 nach minen checken
                 * 
                 *if (punkt17.hasMine() = true)  {
                 *gamestatus = over
                 *}
                 *   else
                 *   
                 *   index++ oder index--
                 *   
                 *   dann je nachdem aufdecken, 
                 *   
                 *   wenn keine nachbarminen--> aufdecken
                 *   wenn nachbarminen ---> int neighbouringmines anzeigen
                 *
                 *     List<CclField> neighbours = I_liFields.Where(  isShown );
                 * 
                 *     IEnumerable<string> query = fruits.Where(fruit => fruit.Length < 6);
                 * 
                 * 






List<CclField>  neighbours =  I_liFields.Where(field => field.X >= (x - 1) && field.X <= (x + 1)
                                    && field.Y >= (y - 1) && field.Y <= (y + 1));


                /*


                x und y koords erfragen

                I_liFields.FindAll(clEntry ==> 




                show  mines


             //edges---

    if (index == 0)
                {
                    if (hasMine(index + 1))
                    {
                        n++;
                    }
                    if (hasMine(index + 10)
                    {
                        n++;
                    }
                    if (hasMine(index + 11))
                    {
                        n++;
                    }
                }
                if (index == 9)
                {
                    if (hasMine(index - 1))
                    {
                        n++;
                    }
                    if (hasMine(index + 6))
                    {
                        n++;
                    }
                    if (hasMine(index + 5))
                    {
                        n++;
                    }
                }
                if (index == 90)
                {
                    if (hasMine(index + 1))
                    {
                        n++;
                    }
                    if (hasMine(index - 10))
                    {
                        n++;
                    }
                    if (hasMine(index - 9))
                    {
                        n++;
                    }
                }
                if (index == 35)
                {
                    if (hasMine(index - 1))
                    {
                        n++;
                    }
                    if (hasMine(index - 10))
                    {
                        n++;
                    }
                    if (hasMines(index - 11))
                    {
                        n++;
                    }
                }

             // Top Row
                if (index > 0 && index < 9)
                {
                    if (hasMine(index - 1))
                    {
                        n++;
                    }
                    if (hasMine(index + 1))
                    {
                        n++;
                    }
                    if (hasMine(index + 9))
                    {
                        n++;
                    }
                    if (hasMine(index + 8))
                    {
                        n++;
                    }
                    if (hasMine(index + 11))
                    {
                        n++;
                    }
                }

                //Bottom Row
                if (index > 90 && index < 99)
                {
                    if (hasMine(index - 1))
                    {
                        n++;

                }

               // left side
                if ((index =


                }

                // Right side
                if ((index == 




                }


                // Middle buttons

                if ((index >  && index < ) || (index >  && index < ) || (index >  && index < ) || (index >  && index < ))
                {

                }

                return

    */






    private void Form1_Load(object sender, EventArgs e)
    {

    }
  }
}
