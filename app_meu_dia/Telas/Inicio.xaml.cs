using System;
using System.Collections.Generic;
using app_meu_dia.Modelos;
using Xamarin.Forms;

namespace app_meu_dia.Telas
{
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();

            DataAtual.Text = DateTime.Now.DayOfWeek.ToString() + ", " + DateTime.Now.ToString("dd/MM");

            CarregarTarefas();
        }

        public void ActionGoCadastro (object sender, EventArgs args)
        {
            Navigation.PushAsync(new Cadastro());    
        }

        private void CarregarTarefas()
        {
            slTarefas.Children.Clear();

            List<Tarefa> lista = new GerenciadorTarefa().Listar();

            int i = 0;

            foreach (Tarefa tarefa in lista)
            {
                LinhaStackLayout(tarefa, i);
                i++;
            }
        }

        public void LinhaStackLayout(Tarefa tarefa, int index)
        {
            Image imgDelete = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("Delete.png") };
            if (Device.RuntimePlatform == Device.UWP)
            {
                imgDelete.Source = ImageSource.FromFile("Resources/Delete.png");
            }
            TapGestureRecognizer DeleteTap = new TapGestureRecognizer();
            DeleteTap.Tapped += delegate
            {
                new GerenciadorTarefa().Deletar(index);
                CarregarTarefas();
            };
            imgDelete.GestureRecognizers.Add(DeleteTap);

            Image imgPrioridade = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("p" + tarefa.Prioridade + ".png") };
            if (Device.RuntimePlatform == Device.UWP)
            {
                imgPrioridade.Source = ImageSource.FromFile("Resources/" + "p" + tarefa.Prioridade + ".png");
            }

            View stackCentral = null;

            if (tarefa.DataFinalizacao == null)
            {
                stackCentral = new Label() { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand, Text = tarefa.Nome };    
            }
            else
            {
                stackCentral = new StackLayout() { VerticalOptions = LayoutOptions.Center, Spacing = 0, HorizontalOptions = LayoutOptions.FillAndExpand };
                ((StackLayout)stackCentral).Children.Add(new Label() { Text = tarefa.Nome, TextColor = Color.Gray });
                ((StackLayout)stackCentral).Children.Add(new Label() { Text = "Finalizado em " + tarefa.DataFinalizacao.Value.ToString("dd/MM/yyyy hh:mm"), TextColor = Color.Gray, FontSize = 10 });
            }



            Image imgCheck = new Image() {VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("CheckOff.png")};
            if (Device.RuntimePlatform == Device.UWP)
            {
                imgCheck.Source = ImageSource.FromFile("Resources/CheckOff.png");
            }
            if (tarefa.DataFinalizacao != null)
            {
                imgCheck.Source = ImageSource.FromFile("CheckOn.png");
                if (Device.RuntimePlatform == Device.UWP)
                {
                    imgCheck.Source = ImageSource.FromFile("Resources/CheckOn.png");
                }
            }


            TapGestureRecognizer CheckTap = new TapGestureRecognizer();
            CheckTap.Tapped += delegate
            {
                new GerenciadorTarefa().Finalizar(index, tarefa);
                CarregarTarefas();
            };
            imgCheck.GestureRecognizers.Add(CheckTap);

            StackLayout linha = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 15 };

            linha.Children.Add(imgCheck);
            linha.Children.Add(stackCentral);
            linha.Children.Add(imgPrioridade);
            linha.Children.Add(imgDelete);

            slTarefas.Children.Add(linha);
        }

    }
}
