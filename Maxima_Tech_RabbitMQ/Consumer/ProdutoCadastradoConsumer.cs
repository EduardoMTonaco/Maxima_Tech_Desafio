using MassTransit;
using MaximaTech.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxima_Tech_RabbitMQ.Consumer
{
    public class ProdutoCadastradoConsumer : IConsumer<ProdutoCadastradoEvent>
    {
        public Task Consume(ConsumeContext<ProdutoCadastradoEvent> context)
        {
            var evento = context.Message;
            Console.WriteLine($"Produto cadastrado: {evento.Nome}, Data: {evento.DataCadastro}");
            return Task.CompletedTask;
        }
    }

}
