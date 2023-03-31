using GoLittleRockstar.Functions;
using GoLittleRockstar.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoLittleRockstar
{
    
    public partial class controlGercekZamanliUretim : UserControl
    {
        public restApi api = new restApi();
        public queries query = new queries();

        public List<clsGercekZamanliUretim> gercekZamanliUretim;
        public controlGercekZamanliUretim()
        {
            InitializeComponent();
        }

        private void btnÜretimVerisiCek_Click(object sender, EventArgs e)
        {
            gercekZamanliUretim = new List<clsGercekZamanliUretim>(); 
            gercekZamanliUretim = api.gercekZamanliUretim(dateBaslangicTarihi.Value, dateBitisTarihi.Value);
            bool insertOrUpdate = false;

            using (var contex = new context())
            {
                foreach(clsGercekZamanliUretim i in gercekZamanliUretim)
                {                   
                    try
                    {
                        contex.tblGercekZamanliUretimler.Add(i);
                        contex.SaveChanges();
                    }
                    catch
                    {
                        insertOrUpdate = true;
                    }
                    finally
                    {
                        if (insertOrUpdate)
                        {
                            contex.tblGercekZamanliUretimler.Update(i);
                            contex.SaveChanges();
                        }
                        insertOrUpdate = false;
                    }
                }
                MessageBox.Show("İşlem Tamamlandı");
            }

        }

        private void btnVeriListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = query.uretimGoster(dateBaslangicTarihi.Value, dateBitisTarihi.Value);
        }
    }
}
