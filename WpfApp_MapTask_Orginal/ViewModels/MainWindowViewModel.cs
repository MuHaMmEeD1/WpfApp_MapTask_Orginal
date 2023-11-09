using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using WpfApp_MapTask_Orginal.Commands;
using WpfApp_MapTask_Orginal.Models;
using Microsoft.Maps.MapControl.WPF;

namespace WpfApp_MapTask_Orginal.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        BBus? bBus;
        List<string>? bus_ids;
        List<string>? bus_cor;

        public BBus? BBuss { get => bBus; set { bBus = value; OnPropertyChinged(); } }
        public List<string>? Bus_ids { get => bus_ids; set { bus_ids = value; OnPropertyChinged(); } }
       


        public RealCommand SecBus { get; set; }

        public string? SelectedBus { get; set; }


        Map map;
        public MainWindowViewModel(Map map)
        {


            SecBus = new RealCommand(_SecBus, _CanSecBus);


         this.map = map;


           


            BBuss = JsonSerializer.Deserialize<BBus>(File.ReadAllText("../../../DataBase//BakuBus.json"));

            Bus_ids = new List<string>();
            





            for (int i = 0; i < BBuss!.BUS.Count; i++)
            {
                bool yoxla = true;

                for (int ib = 0; ib < Bus_ids.Count; ib++)
                {
                    if (Bus_ids[ib] == BBuss.BUS[i].attributes.DISPLAY_ROUTE_CODE) { yoxla = false; break; }
                }



                if (yoxla)
                {
                    Bus_ids.Add(BBuss.BUS[i].attributes.DISPLAY_ROUTE_CODE);

                }
            }




            for (int i = 0; i < BBuss!.BUS.Count; i++)
            {
                Pushpin pushpin = new Pushpin();
                pushpin.Location = new Location(Convert.ToDouble(BBuss.BUS[i].attributes.LATITUDE), Convert.ToDouble(BBuss.BUS[i].attributes.LONGITUDE));
                pushpin.Content = BBuss.BUS[i].attributes.DISPLAY_ROUTE_CODE;

                map.Children.Add(pushpin);
            }





        }




        public bool _CanSecBus(object? par)
        {
            if (SelectedBus == null) { return false; }
            return true;
        }

        public void _SecBus(object? par)
        {
            map.Children.Clear();

        


            for (int i = 0; i < BBuss!.BUS.Count; i++)
            {
                if (BBuss!.BUS[i].attributes.DISPLAY_ROUTE_CODE == SelectedBus) {
                    Pushpin pushpin = new Pushpin();
                    pushpin.Location = new Location(Convert.ToDouble(BBuss.BUS[i].attributes.LATITUDE), Convert.ToDouble(BBuss.BUS[i].attributes.LONGITUDE));
                    pushpin.Content = BBuss.BUS[i].attributes.DISPLAY_ROUTE_CODE;

                    map.Children.Add(pushpin);

                }
            }

            MessageBox.Show(SelectedBus);

        }








        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChinged([CallerMemberName] string? name = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }



    }
}
