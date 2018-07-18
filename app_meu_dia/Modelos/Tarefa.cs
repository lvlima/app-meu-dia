using System;
namespace app_meu_dia.Modelos
{
    public class Tarefa
    {
        public string Nome { get; set; }
        public DateTime? DataFinalizacao { get; set; }
        public byte Prioridade { get; set; }

        public Tarefa()
        {
        }
    }
}
