using MuMaps.Helpers;
using MuMaps.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace MuMaps
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MyTravels : Window
    {
        private List<LocationG> loc1 = new List<LocationG>();
        private List<LocationG> loc2 = new List<LocationG>();
        public MyTravels(string text)
        {
            InitializeComponent();
            _=SearchHelper.GetTokenAsync();
        }




        private void tbx_LocInit_KeyUp(object sender, KeyEventArgs e)
        {
            txbLocs(tbx_LocInit, lst_LocInit);
        }
        private void tbx_LocFin_KeyUp(object sender, KeyEventArgs e)
        {
            txbLocs(tbx_LocFin, lst_LocFin);
        }
        private void txbLocs(TextBox t, ListBox l)
        {
            if (t.Text == string.Empty)
            {
                l.ItemsSource = null;
                return;
            }

            var result = SearchHelper.SearchCity(t.Text);

            if (result == null)
            {
                return;
            }

            var listaLocations = new List<LocationG>();


            foreach (var item in result.items)
            {
                listaLocations.Add(new LocationG()
                {
                    title = item.title,
                    lat = item.position.lat,
                    lng = item.position.lng


                });
            }


            l.ItemsSource = listaLocations;
            if (l.Name.Equals("lst_LocFin"))
            {
                loc2 = listaLocations;
            }
            else
            {
                loc1 = listaLocations;
            }
        }
        private void calcularDistancia(String transport)
        {

            double lat1 = loc1[lst_LocInit.SelectedIndex].lat;
            double lng1 = loc1[lst_LocInit.SelectedIndex].lng;
            double lat2 = loc2[lst_LocFin.SelectedIndex].lat;
            double lng2 = loc2[lst_LocFin.SelectedIndex].lng;
            var result = SearchHelper.generateRoute(lat1, lng1, lat2, lng2, transport);


            if (result == null)
            {
                return;
            }


            var listaRoutes = new List<RouteG>();
            List<Instruction> listainstrucion = new List<Instruction>();
            foreach (var item in result.routes)
            {
                foreach (var item2 in item.sections)
                {
                    foreach (var item3 in item2.actions)
                    {

                        listainstrucion.Add(new Instruction()
                        {
                            desc = item3.instruction
                        });
                    }
                    listaRoutes.Add(new RouteG()
                    {
                        distance = item2.summary.length,
                        time = item2.summary.duration
                    });


                    lst_Indicaciones.ItemsSource = listainstrucion;

                    Int32 tsegundos = item2.summary.duration;
                    Int32 horas = (tsegundos / 3600);
                    Int32 minutos = ((tsegundos-horas*3600)/60);
                    Int32 segundos = tsegundos-(horas*3600+minutos*60);
                    duracion.Text = horas.ToString() + " hora(s) : " + minutos.ToString()
                        + " minuto(s) : " + segundos.ToString()+" segundo(s)";
                    distancia.Text = (item2.summary.length/1000).ToString()+" kilómetro(s)";


                }
            }
        }

        private void calcularc_Click(object sender, RoutedEventArgs e)
        {
            checkRoute();

        }
        private void checkRoute()
        {
            String transport = checkTransport();
            if (lst_LocFin.SelectedValue != null && lst_LocFin.SelectedValue != null && cbTransporte.SelectedIndex>0)

            {
                calcularDistancia(transport);
            }
            else
            {
                lst_Indicaciones.ItemsSource=null;
            }
        }

        private string checkTransport()
        {
            if (cbTransporte.SelectedIndex == 1)
            {
                return "pedestrian";
            }
            else if (cbTransporte.SelectedIndex == 2)
            {
                return "car";
            }
            if (cbTransporte.SelectedIndex == 3)
            {
                return "truck";
            }
            return "";
        }
        private void tbx_LocInit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tbx_LocInit.Text = String.Empty;

        }
    }

}

