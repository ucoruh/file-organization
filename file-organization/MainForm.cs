using System;
using System.Windows.Forms;

namespace FileOrganization
{
    public partial class MainForm : Form
    {
        private const int STORAGE_SIZE = 20;
        private const int DATA_SIZE = 10;
        private const int EMPTY_SLOT = -1;
        private const string EMPTY_SLOT_CHAR = ".";
        private const int MOD_SIZE = 11;

        private int[] _lischDataStorage;
        private int[] _lischDataLinkStorage;
        private int[] _progresiveOverflowDataStorage;
        private int[] _linearQuotientDataStorage;
        private int[] _brentDataStorage;

        private int[] _dataList;
        private int _dataListIndex;

       
        public MainForm()
        {
            InitializeComponent();

            #region DEFINE DATA LIST
            _dataList = new int[DATA_SIZE];
            #endregion

            #region DEFINE STORAGE LISTS
            _lischDataStorage       = new int[STORAGE_SIZE];
            _lischDataLinkStorage   = new int[STORAGE_SIZE];
            _progresiveOverflowDataStorage   = new int[STORAGE_SIZE];
            _linearQuotientDataStorage     = new int[STORAGE_SIZE];
            _brentDataStorage       = new int[STORAGE_SIZE];
            #endregion

            #region RESET DATA LIST
            for (int index = 0; index < DATA_SIZE; index++){
                _dataList[index] = EMPTY_SLOT;
            }

            _dataListIndex = 0;
            #endregion

            #region RESET STORAGE LIST

            for (int index = 0; index < STORAGE_SIZE; index++) 
            {
                _lischDataStorage[index] = EMPTY_SLOT;
                _lischDataLinkStorage[index] = EMPTY_SLOT;
                _progresiveOverflowDataStorage[index] = EMPTY_SLOT;
                _linearQuotientDataStorage[index] = EMPTY_SLOT;
                _brentDataStorage[index] = EMPTY_SLOT;
                
               
                listboxLisch.Items.Add(EMPTY_SLOT_CHAR);
                listboxProgressiveoverflow.Items.Add(EMPTY_SLOT_CHAR);
                listboxLinearQuotient.Items.Add(EMPTY_SLOT_CHAR);
                listboxBrentsMethod.Items.Add(EMPTY_SLOT_CHAR);
                listboxLischLink.Items.Add(EMPTY_SLOT_CHAR);


            }
            #endregion
        }

        private void btnClearAllDataList_Click(object sender, EventArgs e)
        {
            listboxDataList.Items.Clear();

            for (int index = 0; index < DATA_SIZE; index++){
                _dataList[index] = EMPTY_SLOT;
            }

            _dataListIndex = 0;
            
            txtDataInput.Text = "";
            
            label13.Text = "0";
        }

        private void btnProcessLisch_Click(object sender, EventArgs e)
        {
            //LISCH  
            //lısch te dizi ile ona bağlı çakışmları dizinin sonundan eklemeye başlayarak 
            //çakışma yazılır.

            richtextboxLog.SelectAll();
            richtextboxLog.Clear();

            bntClearLisch.PerformClick();

            richtextboxLog.AppendText("################### LISCH BEGIN ###################\r\n");

            int indexLisch = 0;
            int keyindex = 0;

            int stepcounter = 0;

            int klisch = 0;

            while (indexLisch < DATA_SIZE)
            {
                keyindex = _dataList[indexLisch] % MOD_SIZE;

                if(keyindex == EMPTY_SLOT)
                { 
                    indexLisch++;
                    continue;
                }

                if (_lischDataStorage[keyindex] == EMPTY_SLOT)
                {
                    _lischDataStorage[keyindex] = _dataList[indexLisch];

                    richtextboxLog.AppendText("[Step-"+stepcounter+"] Data " + _dataList[indexLisch] + " is located on " + keyindex + ", No Collision\r\n");

                    stepcounter++;
                }
                else
                {

                    richtextboxLog.AppendText("Data " + _dataList[indexLisch] + " is located on " + keyindex + ", Collision Detected Looking for Empty Slot from End of The List\r\n");

                    for (klisch = keyindex; klisch < STORAGE_SIZE; klisch++)
                    {
                        if (_lischDataStorage[klisch] == EMPTY_SLOT)
                        {
                            richtextboxLog.AppendText("[Step-"+stepcounter+"] Index " + klisch + " is empty, stopping search\r\n");

                            break;
                        }
                        else
                        {
                            richtextboxLog.AppendText("[Step-"+stepcounter+"] Index " + klisch + " is not empty, jumping to next slot\r\n");
                        }

                        stepcounter++;
                    }

                    if(klisch == STORAGE_SIZE)
                    {
                        richtextboxLog.AppendText("[Step-"+stepcounter+"] Data " + _dataList[indexLisch] + " is cannot located there is no empty slot\r\n");
                    }
                    else
                    {
                        _lischDataStorage[klisch] = _dataList[indexLisch];
                        _lischDataLinkStorage[klisch] = keyindex;

                        richtextboxLog.AppendText("[Step-"+stepcounter+"] Data " + _dataList[indexLisch] + " is located on " + klisch + " with collision\r\n");

                    }
                }
                indexLisch++;
            }
            
            listboxLisch.Items.Clear();
            listboxLischLink.Items.Clear();

            for (int y = 0; y < STORAGE_SIZE; y++)
            {
                if (_lischDataStorage[y] == EMPTY_SLOT)
                {
                    listboxLisch.Items.Add(y.ToString() + "-> null");
                    listboxLischLink.Items.Add(y.ToString() + "-> null"); continue;
                }
                listboxLisch.Items.Add(y.ToString() + "->" + _lischDataStorage[y]);
                listboxLischLink.Items.Add(y.ToString() + "->" + _lischDataLinkStorage[y]);
            }

            label14.Text = stepcounter.ToString() + "  step";

            richtextboxLog.AppendText("################### LISCH END ###################\r\n");
        }

        private void btnGenerateAllRandomValues_Click(object sender, EventArgs e)
        {
            Random randm = new Random();
            
            int randnumber;

            listboxDataList.Items.Clear();

            for (_dataListIndex = 0; _dataListIndex < DATA_SIZE;)
            {
                randnumber = randm.Next(50);

                _dataList[_dataListIndex] = randnumber;

                listboxDataList.Items.Add(randnumber.ToString());
                
                _dataListIndex++;
            }
        }

        private void bntClearLisch_Click(object sender, EventArgs e)
        {
            listboxLisch.Items.Clear();
            listboxLischLink.Items.Clear();

            for (int index  = 0; index < STORAGE_SIZE; index++)
            {
                _lischDataStorage[index] = EMPTY_SLOT;
                _lischDataLinkStorage[index] = EMPTY_SLOT;

                listboxLisch.Items.Add(EMPTY_SLOT_CHAR);
                listboxLischLink.Items.Add(EMPTY_SLOT_CHAR);

            }
        }

        private void btnGenerateRandomValue_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDataInput.Text))
            {
                if (_dataListIndex < DATA_SIZE)
                {
                    listboxDataList.Items.Add(txtDataInput.Text);

                    _dataList[_dataListIndex] = Int32.Parse(txtDataInput.Text);

                    _dataListIndex++;

                    label13.Text = _dataListIndex.ToString();

                    txtDataInput.ResetText();
                }
                else
                {
                    txtDataInput.Text = "Record memory is full";

                }
            }
        }

        private void bntClearProgressiveOverflow_Click(object sender, EventArgs e)
        {
            listboxProgressiveoverflow.Items.Clear();

            for (int index = 0; index < STORAGE_SIZE; index++)
            {
                _progresiveOverflowDataStorage[index] = EMPTY_SLOT;

                listboxProgressiveoverflow.Items.Add(EMPTY_SLOT_CHAR);

            }
        }

        private void btnProcessProgresiveOverflow_Click(object sender, EventArgs e)
        {
            richtextboxLog.SelectAll();
            richtextboxLog.Clear();

            bntClearProgressiveOverflow.PerformClick();

            richtextboxLog.AppendText("################### PROGRESSIVE OVERFLOW BEGIN ###################\r\n");

            int stepcounter = 0;
            //PROGRESSİVE OVERFLOW
            //progresiv overflow da çakışan yerler için 1 artırarak boş yere yazılır.

            int indexProgres = 0;
            int keyindexProg = -1;

            while (indexProgres < DATA_SIZE)
            {
                keyindexProg = _dataList[indexProgres] % MOD_SIZE;

                if (keyindexProg == EMPTY_SLOT)
                {
                    indexProgres++;
                    continue;
                }

                if (_progresiveOverflowDataStorage[keyindexProg] == EMPTY_SLOT)
                {
                    _progresiveOverflowDataStorage[keyindexProg] = _dataList[indexProgres];

                    stepcounter++;

                    richtextboxLog.AppendText("[Step-"+stepcounter+"] Data " + _dataList[indexProgres] + " is located on " + keyindexProg + ", No Collision\r\n");
                }
                else
                {

                    richtextboxLog.AppendText("[Step-"+stepcounter+"] Data " + _dataList[indexProgres] + " is located on " + keyindexProg + ", Collision Detected Looking for Empty Slot\r\n");

                    int index = 0;

                    for (index = 0; index < STORAGE_SIZE; index++)
                    {
                        if (_progresiveOverflowDataStorage[index] == EMPTY_SLOT)
                        {
                            richtextboxLog.AppendText("[Step-"+stepcounter+"] Index " + index + " is empty, stopping search\r\n");

                            break;
                        }
                        else
                        {
                            richtextboxLog.AppendText("[Step-"+stepcounter+"] Index " + index + " is not empty, jumping to next slot\r\n");
                        }

                        stepcounter++;
                    }


                    if (index == STORAGE_SIZE)
                    {
                        richtextboxLog.AppendText("[Step-"+stepcounter+"] Data " + _dataList[indexProgres] + " is cannot located there is no empty slot\r\n");
                    }
                    else
                    {
                        _progresiveOverflowDataStorage[index] = _dataList[indexProgres];

                        richtextboxLog.AppendText("[Step-"+stepcounter+"] Data " + _dataList[indexProgres] + " is located on " + index + " with collision\r\n");

                    }

                }
                indexProgres++;
            }

            listboxProgressiveoverflow.Items.Clear();


            for (int y = 0; y < STORAGE_SIZE; y++)
            {

                if (_progresiveOverflowDataStorage[y] == EMPTY_SLOT)
                {
                    listboxProgressiveoverflow.Items.Add(y.ToString() + "-> null");
                    continue;
                }

                listboxProgressiveoverflow.Items.Add(y.ToString() + "->" + _progresiveOverflowDataStorage[y]);

            }


            lblStepCountProgressiveOverflow.Text = stepcounter.ToString() + "  step";

            richtextboxLog.AppendText("################### PROGRESSIVE OVERFLOW END ###################\r\n");
        }

        private void listboxLisch_SelectedIndexChanged(object sender, EventArgs e)
        {
            listboxLischLink.SelectedIndex = listboxLisch.SelectedIndex;
        }

        private void btnProcessLinearQuotient_Click(object sender, EventArgs e)
        {

            richtextboxLog.SelectAll();
            richtextboxLog.Clear();

            btnClearLinearQuotient.PerformClick();

            richtextboxLog.AppendText("################### LINEAR QUOTIENT BEGIN ###################\r\n");


            //Linear Quetient
            //artırım yaparken değişkene göre tam kısımlar dikkate lınara değişik bir arrtırm var.
            //lısch te dizi ile ona bağlı çakışmları dizinin sonundan eklemeye başlayarak 
            //çakışma yazılır.
            int stepcounter = 0;
            int indexLinerQ = 0;
            int keyindex = -1;
            int increments;
            int kLinearQ;

            while (indexLinerQ < DATA_SIZE)
            {
                increments = 0;

                keyindex = _dataList[indexLinerQ] % MOD_SIZE;

                if (keyindex == EMPTY_SLOT)
                {
                    indexLinerQ++;
                    continue;
                }

                increments = (_dataList[indexLinerQ] / MOD_SIZE) % MOD_SIZE;

                if (_linearQuotientDataStorage[keyindex] == EMPTY_SLOT)
                {
                    _linearQuotientDataStorage[keyindex] = _dataList[indexLinerQ];

                    stepcounter++;

                    richtextboxLog.AppendText("[Step-"+stepcounter+"] Data " + _dataList[indexLinerQ] + " is located on " + keyindex + ", No Collision\r\n");

                }
                else
                {
                    richtextboxLog.AppendText("[Step-"+stepcounter+"] Data " + _dataList[indexLinerQ] + " is located on " + keyindex + ", Collision Detected Looking for Empty Slot\r\n");


                    for (kLinearQ = 0; kLinearQ < STORAGE_SIZE;)
                    {
                        if (_linearQuotientDataStorage[kLinearQ] == EMPTY_SLOT)
                        {
                            richtextboxLog.AppendText("[Step-"+stepcounter+"] Index " + kLinearQ + " is empty, stopping search\r\n");

                            break;
                        }
                        else
                        {
                            kLinearQ +=increments;

                            richtextboxLog.AppendText("[Step-"+stepcounter+"] Index " + kLinearQ + " is not empty, jumping to next slot with increment "+ increments + "\r\n");

                            if (increments == 0)
                            {
                                break;
                            }

                            stepcounter++;
                        }
  
                    }

                    if (kLinearQ < STORAGE_SIZE)
                    {
                        _linearQuotientDataStorage[kLinearQ] = _dataList[indexLinerQ];

                        richtextboxLog.AppendText("[Step-"+stepcounter+"] Data " + _dataList[indexLinerQ] + " is located on " + kLinearQ + " with collision\r\n");
                    }
                    else
                    {
                        richtextboxLog.AppendText("[Step-"+stepcounter+"] Data " + _dataList[indexLinerQ] + " is cannot located there is no empty slot\r\n");

                        break;
                    }

                    indexLinerQ++;
                }
            }

            listboxLinearQuotient.Items.Clear();

            for (int y = 0; y < STORAGE_SIZE; y++)
            {
                if (_linearQuotientDataStorage[y] == EMPTY_SLOT)
                {
                    listboxLinearQuotient.Items.Add(y.ToString() + "-> null"); continue;
                }
                listboxLinearQuotient.Items.Add(y.ToString() + "->" + _linearQuotientDataStorage[y]);

            }

            lblLinearQStepSize.Text = stepcounter.ToString() + " step";

            richtextboxLog.AppendText("################### LINEAR QUOTIENT END ###################\r\n");

        }

        private void btnProcessBrentMethod_Click(object sender, EventArgs e)
        {
            // BRENT'S METHOD 

            richtextboxLog.SelectAll();
            richtextboxLog.Clear();

            bntClearBrent.PerformClick();

            richtextboxLog.AppendText("################### BRENT'S METHOD BEGIN ###################\r\n");

            int stepcounter = 0;

            int indexBrents = 0;

            int incrementB;
            int incremenTempB;
            int stepA = 0;
            int stepB = 0;
            int k1Brent;
            int k2Brent;

            int keyindexB = 0;

            while (indexBrents < DATA_SIZE)// 10 tane sabit değer için kontrol et.
            {
                keyindexB = _dataList[indexBrents] % MOD_SIZE;

                if (keyindexB == EMPTY_SLOT)
                {
                    indexBrents++;
                    continue;
                }

                incrementB = (_dataList[indexBrents] / MOD_SIZE) % MOD_SIZE;

                if (_brentDataStorage[keyindexB] == EMPTY_SLOT)// hash değerindeki adres boş ise
                {
                    _brentDataStorage[keyindexB] = _dataList[indexBrents];// oraya direk yaz.

                    stepcounter++;

                    richtextboxLog.AppendText("[Step-"+stepcounter+"]Data " + _dataList[indexBrents] + " is located on " + keyindexB + ", No Collision\r\n");

                }
                else// boş değilse orası
                {

                    richtextboxLog.AppendText("[Step-"+stepcounter+"] Data " + _dataList[indexBrents] + " is located on " + keyindexB + ", Collision Detected Looking for Empty Slot\r\n");

                    for (k1Brent = keyindexB; k1Brent < STORAGE_SIZE;)//oradan artırarak koyacağın yerin adım sayısına bak.
                    {
                        if (_brentDataStorage[k1Brent] == EMPTY_SLOT)
                        {
                            richtextboxLog.AppendText("[Step-"+stepcounter+"] Index " + k1Brent + " is empty, stopping search\r\n");

                            break;
                        }
                        else
                        {

                            richtextboxLog.AppendText("[Step-"+stepcounter+"] Index " + k1Brent + " is not empty, jumping to next slot with increment "+ incrementB +"\r\n");

                            k1Brent += incrementB;

                            if (incrementB == 0)
                            {
                                incrementB = 1;
                            }

                            stepA++;// adım sayısı
                        }

                    }
                   
                    incremenTempB = (_brentDataStorage[keyindexB] / MOD_SIZE) % MOD_SIZE;// artışı baştan hesapla

                   
                    for (k2Brent = keyindexB; k2Brent < STORAGE_SIZE;) // oradaki değeri kaydırsam olacak adım sayısı
                    {
                        if (_brentDataStorage[k2Brent] == EMPTY_SLOT)
                        {

                            richtextboxLog.AppendText("[Step-"+stepcounter+"] Index " + k2Brent + " is not empty, jumping to next slot with increment " + incremenTempB + "\r\n");

                            break;
                        }
                        else
                        {
                            k2Brent += incremenTempB;

                            richtextboxLog.AppendText("[Step-"+stepcounter+"] Index " + k2Brent + " is not empty, jumping to next slot with increment " + incremenTempB + "\r\n");

                            if (incremenTempB == 0)
                            {
                                incremenTempB = 1;
                            }

                            stepB++;
                        }

                    }

                    if ((stepB < stepA) &&
                        (k1Brent < STORAGE_SIZE) &&
                        (indexBrents < DATA_SIZE))// yer değiştrimeye gerek yok...
                    {
                        _brentDataStorage[k1Brent] = _dataList[indexBrents];

                        stepcounter = stepcounter + stepA;

                        richtextboxLog.AppendText("[Step-"+stepcounter+"] First option " + k1Brent + " is better choise then Second option "+ k2Brent + "\r\n");
                        richtextboxLog.AppendText("[Step-"+stepcounter+"] First option step size " + stepA + ", Second option step size " + stepB + "\r\n");
                        richtextboxLog.AppendText("[Step-"+stepcounter+"] No Replacement Required\r\n");

                    }

                    if ((stepB >= stepA) &&
                        ((keyindexB + k2Brent) < STORAGE_SIZE) &&
                        (keyindexB < DATA_SIZE)) // değiştirme işlemine ihtiyaç var
                    {

                        richtextboxLog.AppendText("[Step-"+stepcounter+"] Second option " + k2Brent + " is better choise then First option " + k1Brent + "\r\n");
                        richtextboxLog.AppendText("[Step-"+stepcounter+"] First option step size " + stepA + ", Second option step size " + stepB + "\r\n");
                        richtextboxLog.AppendText("[Step-"+stepcounter+"] Replacement Required\r\n");


                        richtextboxLog.AppendText("[Step-"+stepcounter+"] Index " + keyindexB + " moved to  " + (keyindexB + k2Brent) + "\r\n");

                        richtextboxLog.AppendText("[Step-"+stepcounter+"] New Data at " + indexBrents + " in the list, located on index " + keyindexB + "\r\n");

                        _brentDataStorage[keyindexB + k2Brent] = _brentDataStorage[keyindexB];

                        _brentDataStorage[keyindexB] = _dataList[indexBrents];

                        stepcounter = stepcounter + stepB;
                    }

                    // tempe al ve bekle; 
                }

                indexBrents++; // bir sonraki kayıta bak.
            }

            listboxBrentsMethod.Items.Clear();

            for (int y = 0; y < STORAGE_SIZE; y++)
            {
                if (_brentDataStorage[y] == EMPTY_SLOT)
                {
                    listboxBrentsMethod.Items.Add(y.ToString() + "-> null");
                    continue;
                }

                listboxBrentsMethod.Items.Add(y.ToString() + "->" + _brentDataStorage[y]);

            }

           
            label17.Text = stepcounter.ToString() + " step";

            richtextboxLog.AppendText("################### BRENT'S METHOD END ###################\r\n");
        }

        private void listboxLischLink_SelectedIndexChanged(object sender, EventArgs e)
        {
            listboxLisch.SelectedIndex = listboxLischLink.SelectedIndex;
        }

        private void bntClearLinearQuotient_Click(object sender, EventArgs e)
        {
            listboxLinearQuotient.Items.Clear();

            for (int index = 0; index < STORAGE_SIZE; index++)
            {
                _linearQuotientDataStorage[index] = EMPTY_SLOT;

                listboxLinearQuotient.Items.Add(EMPTY_SLOT_CHAR);
            }
        }

        private void bntClearBrent_Click(object sender, EventArgs e)
        {
            listboxBrentsMethod.Items.Clear();
            
            for (int index = 0; index < STORAGE_SIZE; index++)
            {
                _brentDataStorage[index] = EMPTY_SLOT;

                listboxBrentsMethod.Items.Add(EMPTY_SLOT_CHAR);
               
            }
        }
    }
}