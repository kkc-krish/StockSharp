﻿namespace StockSharp.Messages
{
	using System;
	using System.Collections.Generic;

	using Ecng.Common;
	using Ecng.Serialization;

	using StockSharp.Logging;

	/// <summary>
	/// Интерфейс, описывающий адаптер, конвертирующий сообщения <see cref="Message"/> в команды торговой системы и обратно.
	/// </summary>
	public interface IMessageAdapter : IMessageChannel, IPersistable, ILogReceiver
	{
		/// <summary>
		/// Генератор идентификаторов транзакций.
		/// </summary>
		IdGenerator TransactionIdGenerator { get; }

		/// <summary>
		/// <see langword="true"/>, если сессия используется для получения маркет-данных, иначе, <see langword="false"/>.
		/// </summary>
		bool IsMarketDataEnabled { get; set; }

		/// <summary>
		/// <see langword="true"/>, если сессия используется для отправки транзакций, иначе, <see langword="false"/>.
		/// </summary>
		bool IsTransactionEnabled { get; set; }

		/// <summary>
		/// Проверить введенные параметры на валидность.
		/// </summary>
		bool IsValid { get; }

		/// <summary>
		/// Объединять обработчики входящих сообщений для адаптеров.
		/// </summary>
		bool JoinInProcessors { get; }

		/// <summary>
		/// Объединять обработчики исходящих сообщений для адаптеров.
		/// </summary>
		bool JoinOutProcessors { get; }

		/// <summary>
		/// Описание классов инструментов, в зависимости от которых будут проставляться параметры в <see cref="SecurityMessage.SecurityType"/> и <see cref="SecurityId.BoardCode"/>.
		/// </summary>
		IDictionary<string, RefPair<SecurityTypes, string>> SecurityClassInfo { get; }

		/// <summary>
		/// Настройки механизма отслеживания соединений.
		/// </summary>
		MessageAdapterReConnectionSettings ReConnectionSettings { get; }

		/// <summary>
		/// Интервал оповещения сервера о том, что подключение еще живое. По-умолчанию равно 1 минуте.
		/// </summary>
		TimeSpan HeartbeatInterval { get; set; }

		/// <summary>
		/// Интервал генерации сообщения <see cref="TimeMessage"/>. По-умолчанию равно 10 миллисекундам.
		/// </summary>
		TimeSpan MarketTimeChangedInterval { get; set; }

		/// <summary>
		/// Являются ли подключения адаптеров независимыми друг от друга.
		/// </summary>
		bool IsAdaptersIndependent { get; }

		/// <summary>
		/// Создавать объединенный инструмент для инструментов с разных торговых площадок.
		/// </summary>
		bool CreateAssociatedSecurity { get; set; }

		/// <summary>
		/// Обновлять стакан для инструмента при появлении сообщения <see cref="Level1ChangeMessage"/>.
		/// </summary>
		bool CreateDepthFromLevel1 { get; set; }

		/// <summary>
		/// Код площадки для объединенного инструмента.
		/// </summary>
		string AssociatedBoardCode { get; set; }

		/// <summary>
		/// Требуется ли дополнительное сообщение <see cref="PortfolioLookupMessage"/> для получения списка портфелей и позиций.
		/// </summary>
		bool PortfolioLookupRequired { get; }

		/// <summary>
		/// Требуется ли дополнительное сообщение <see cref="SecurityLookupMessage"/> для получения списка инструментов.
		/// </summary>
		bool SecurityLookupRequired { get; }

		/// <summary>
		/// Требуется ли дополнительное сообщение <see cref="OrderStatusMessage"/> для получения списка заявок и собственных сделок.
		/// </summary>
		bool OrderStatusRequired { get; }

		/// <summary>
		/// Добавить <see cref="Message"/> в исходящую очередь <see cref="IMessageAdapter"/>.
		/// </summary>
		/// <param name="message">Сообщение.</param>
		void SendOutMessage(Message message);

		/// <summary>
		/// Обработчик входящих сообщений.
		/// </summary>
		IMessageProcessor InMessageProcessor { get; set; }

		/// <summary>
		/// Обработчик исходящих сообщений.
		/// </summary>
		IMessageProcessor OutMessageProcessor { get; set; }

		/// <summary>
		/// Создать для заявки типа <see cref="OrderTypes.Conditional"/> условие, которое поддерживается подключением.
		/// </summary>
		/// <returns>Условие для заявки. Если подключение не поддерживает заявки типа <see cref="OrderTypes.Conditional"/>, то будет возвращено null.</returns>
		OrderCondition CreateOrderCondition();
	}
}