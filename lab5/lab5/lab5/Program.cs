using System;
using System.IO;

namespace lab5
{
    class Program
    {
        /// <summary>
        /// Основное тело программы, отвечающее за ее работоспособность
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                string ConcretePlayer, ConcreteClub;
                Console.WriteLine("Введите фамилию игрока:");
                ConcretePlayer = Console.ReadLine();
                Console.WriteLine("Введите название клуба:");
                ConcreteClub = Console.ReadLine();
                SportDirMediator mediator = new SportDirMediator();
                Colleague customer = new CustomerColleague(mediator);
                Colleague Agent = new AgentColleague(mediator);
                Colleague Player = new PlayerColleague(mediator);
                mediator.Customer = customer;
                mediator.Agent = Agent;
                mediator.Player = Player;
                customer.Send("В Вашем клиенте " + ConcretePlayer + " заинтересован другой клуб - " + ConcreteClub + ".");
                Agent.Send("Привет, тобой заинтересован " + ConcreteClub + "!");
                Player.Send("Поздравляем, " + ConcretePlayer + " согласен перейти к вам!");
            }
            catch
            {
                Console.WriteLine("Неверно введены данные!");
            }
            Console.ReadLine();
        }
    }
    /// <summary>
    /// Абстрактный класс посредника
    /// </summary>
    abstract class Mediator
    {
        /// <summary>
        /// Процедура для вывода сообщения определенному участнику сделки.
        /// </summary>
        /// <param name="msg">Само уведомление</param>
        /// <param name="colleague">Кому отправляется</param>
        public abstract void Send(string msg, Colleague colleague);
    }
    /// <summary>
    /// Абстрактный класс участника сделки для наследования
    /// </summary>
    abstract class Colleague
    {
       Mediator mediator;
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="mediator">посредник для взаимодействия</param>
        public Colleague(Mediator mediator)
        {
            this.mediator = mediator;
        }
        /// <summary>
        /// Процедура для отправки уведомления
        /// </summary>
        /// <param name="message">Сообщение одному из участников сделки</param>
        public virtual void Send(string message)
        {
            mediator.Send(message, this);
        }
        /// <summary>
        /// процедура для наследования остальными коллегами(участниками) сделки
        /// </summary>
        /// <param name="message">Выводимое сообщение</param>
        public abstract void Notify(string message);
    }
    /// <summary>
    /// класс клуба-покупателя
    /// </summary>
    class CustomerColleague : Colleague
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="mediator">посредник для взаимодействия</param>
        public CustomerColleague(Mediator mediator)
            : base(mediator)
        { }
        /// <summary>
        /// Процедура для вывода сообщения
        /// </summary>
        /// <param name="message">выводимое сообщение</param>
        public override void Notify(string message)
        {
            Console.WriteLine("Сообщение клубу-покупателю: " + message);
        }
    }
    /// <summary>
    /// Класс агента
    /// </summary>
    class AgentColleague : Colleague
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="mediator">посредник для взаимодействия</param>
        public AgentColleague(Mediator mediator)
            : base(mediator)
        { }
        /// <summary>
        /// Процедура для уведомления участника сделки
        /// </summary>
        /// <param name="message">выводимое сообщение</param>
        public override void Notify(string message)
        {
            Console.WriteLine("Сообщение агенту: " + message);
        }
    }
    /// <summary>
    /// класс игрока
    /// </summary>
    class PlayerColleague : Colleague
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="mediator">Посредник для взаимодействия</param>
        public PlayerColleague(Mediator mediator)
            : base(mediator)
        { }
        /// <summary>
        /// Процедура уведомления участника сделки
        /// </summary>
        /// <param name="message">Выводимое сообщение</param>
        public override void Notify(string message)
        {
            Console.WriteLine("Сообщение игроку: " + message);
        }
    }
    /// <summary>
    /// Класс спортивного директора(Конкретный посредник)
    /// </summary>
    class SportDirMediator : Mediator
    {
        /// <summary>
        /// Конструктор для клуба-покупателя
        /// </summary>
        public Colleague Customer { get; set; }
        /// <summary>
        /// Конструктор для агента
        /// </summary>
        public Colleague Agent { get; set; }
        /// <summary>
        /// конструктор для игрока
        /// </summary>
        public Colleague Player { get; set; }
        /// <summary>
        /// Процедура Вывода сообщения на экран
        /// </summary>
        /// <param name="msg">Выводимое сообщение</param>
        /// <param name="colleague">Кому предназначено сообщение</param>
        public override void Send(string msg, Colleague colleague)
        {
            if (Customer == colleague)
                Agent.Notify(msg);
            else if (Agent == colleague)
                Player.Notify(msg);
            else if (Player == colleague)
                Customer.Notify(msg);
        }
    }
}
