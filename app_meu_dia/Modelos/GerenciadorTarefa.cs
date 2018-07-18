using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace app_meu_dia.Modelos
{
    public class GerenciadorTarefa
    {
        private List<Tarefa> Lista { get; set; }

        public GerenciadorTarefa()
        {
        }

        public void Salvar(Tarefa tarefa)
        {
            Lista = Listar();
            Lista.Add(tarefa);
            SalvarNoProperties(Lista);
        }

        public void Deletar(int index)
        {
            Lista = Listar();
            Lista.RemoveAt(index);
            SalvarNoProperties(Lista);
        }

        public void Finalizar(int index, Tarefa tarefa)
        {
            Lista = Listar();
            Lista.RemoveAt(index);

            tarefa.DataFinalizacao = DateTime.Now;
            Lista.Add(tarefa);
            SalvarNoProperties(Lista);
        }

        public List<Tarefa> Listar()
        {
            return ListarDoProperties();
        }

        private void SalvarNoProperties(List<Tarefa> lista)
        {
            if (App.Current.Properties.ContainsKey("Tarefas"))
            {
                App.Current.Properties.Remove("Tarefas");
            }

            string JsonVal = JsonConvert.SerializeObject(lista);

            App.Current.Properties.Add("Tarefas", JsonVal);
        }

        private List<Tarefa> ListarDoProperties()
        {
            if (App.Current.Properties.ContainsKey("Tarefas"))
            {
                string JsonVal = (String)App.Current.Properties["Tarefas"];

                List<Tarefa> lista = JsonConvert.DeserializeObject <List<Tarefa>>(JsonVal);

                return lista;
            }

            return new List<Tarefa>();
        }

    }
}
