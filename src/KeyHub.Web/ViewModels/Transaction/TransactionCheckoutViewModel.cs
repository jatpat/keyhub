﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KeyHub.Web.ViewModels.Customer;
using KeyHub.Data;

namespace KeyHub.Web.ViewModels.Transaction
{
    /// <summary>
    /// Viewmodel for checkout of a Transaction
    /// </summary>
    public class TransactionCheckoutViewModel : RedirectUrlModel
    {
        public TransactionCheckoutViewModel() : base() { }

        /// <summary>
        /// Construct the viewmodel
        /// </summary>
        /// <param name="transaction">Transaction to checkout</param>
        /// <param name="purchasingCustomers">Customers to select as purchaser</param>
        /// <param name="owningCustomers">Customers to select as owner</param>
        /// <param name="countries">List of countries to select when creating a new customer</param>
        public TransactionCheckoutViewModel(Model.Transaction transaction, List<Model.Customer> owningCustomers, 
            List<Model.Customer> purchasingCustomers, List<Model.Country> countries)
        {
            Transaction = new TransactionViewModel(transaction);
            OwningCustomers = owningCustomers.ToSelectList(x => x.ObjectId, x => x.Name);
            PurchasingCustomers = purchasingCustomers.ToSelectList(x => x.ObjectId, x => x.Name);
            
            //Set default selected Owning and Purchasing Customer
            //PurchasingCustomerId=..
            //OwningCustomerId=..
            ExistingPurchasingCustomer = false;
            ExistingOwningCustomer = false;
            OwningCustomerIsPurchasingCustomerId = true;
            NewPurchasingCustomer = new CustomerCreateViewModel(countries);
            NewOwningCustomer = new CustomerCreateViewModel(countries);
        }

        /// <summary>
        /// Convert back to Transaction instance
        /// </summary>
        /// <param name="original">Original Transaction.</param>
        /// <returns>Transaction containing viewmodel data </returns>
        public Model.Transaction ToEntity(Model.Transaction original)
        {
            return Transaction.ToEntity(original);
        }

        /// <summary>
        /// Creating Transaction
        /// </summary>
        public TransactionViewModel Transaction { get; set; }

        /// <summary>
        /// Customers to select as purchaser
        /// </summary>
        public SelectList PurchasingCustomers { get; set; }

        /// <summary>
        /// Id of selected purchasing customer
        /// </summary>
        public Guid PurchasingCustomerId { get; set; }

        /// <summary>
        /// Use an existing Purchasing Customer
        /// </summary>
        public Boolean ExistingPurchasingCustomer { get; set; }

        /// <summary>
        /// New purchasing customer details
        /// </summary>
        public CustomerCreateViewModel NewPurchasingCustomer { get; set; }

        /// <summary>
        /// Customers to select as owner
        /// </summary>
        public SelectList OwningCustomers { get; set; }

        /// <summary>
        /// Id of selected owning customer
        /// </summary>
        public Guid OwningCustomerId { get; set; }

        /// <summary>
        /// Create a new customer as Owning Customer
        /// </summary>
        public Boolean ExistingOwningCustomer { get; set; }

        /// <summary>
        /// New owning customer details
        /// </summary>
        public CustomerCreateViewModel NewOwningCustomer { get; set; }

        /// <summary>
        /// Owning customer is the same as purchasing customer
        /// </summary>
        public Boolean OwningCustomerIsPurchasingCustomerId { get; set; }
    }
}