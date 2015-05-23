/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); 
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
*/

using System;
using QuantConnect.Orders;
using QuantConnect.Securities.Interfaces;

namespace QuantConnect.Brokerages
{
    /// <summary>
    /// Models brokerage transactions, fees, and order
    /// </summary>
    public interface IBrokerageModel
    {
        /// <summary>
        /// Returns true if the brokerage could accept this order. This takes into account
        /// order type, security type, order size limits, and time of day.
        /// </summary>
        /// <remarks>
        /// For example, a brokerage may have no connectivity at certain times, or an order rate/size limit
        /// </remarks>
        /// <param name="time">The current time</param>
        /// <param name="order">The order to be processed</param>
        /// <param name="message"></param>
        /// <returns>True if the brokerage could process the order, false otherwise</returns>
        bool CanSubmitOrder(DateTime time, Order order, out BrokerageMessageEvent message);

        /// <summary>
        /// Returns true if the brokerage would be able to execute this order at this time assuming
        /// market prices are sufficient for the fill to take place. This is used to emulate the 
        /// brokerage fills in backtesting and paper trading. For example some brokerages may not perform
        /// executions during extended market hours. This is not intended to be checking whether or not
        /// the exchange is open, that is handled in the Security.Exchange property.
        /// </summary>
        /// <param name="time">The current time</param>
        /// <param name="order">The order to test for execution</param>
        /// <returns>True if the brokerage would be able to perform the execution, false otherwise</returns>
        bool CanExecuteOrder(DateTime time, Order order);

        /// <summary>
        /// Gets a new transaction model the represents this brokerage's fee structure and fill behavior
        /// </summary>
        /// <param name="symbol">The symbol whose transaction model we seek</param>
        /// <param name="securityType">The security type whose transaction model we seek</param>
        /// <returns>The transaction model for this brokerage</returns>
        ISecurityTransactionModel GetTransactionModel(string symbol, SecurityType securityType);
    }
}
