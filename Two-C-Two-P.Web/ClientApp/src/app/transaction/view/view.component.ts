import { Component, OnInit } from "@angular/core";
import { Transaction } from "../models/transaction.model";
import { TransactionService } from '../transaction.service';

@Component({
  selector: "app-view-transactions",
  templateUrl: "./view.component.html",
})
export class ViewComponent implements OnInit {
  transactions: Transaction[];
  //columnDefs = [
  //  { headerName: "Make", field: "make" },
  //  { headerName: "Model", field: "model" },
  //  { headerName: "Price", field: "price" }
  //];

  columnDefs = [
    { headerName: "Identifier", field: "id" },
    { headerName: "Amount", field: "amount" },
    { headerName: "Currency", field: "currency" },
    { headerName: "Transaction Date", field: "transactionDate" },
    { headerName: "Status", field: "status" }
  ];

  rowData: any;


  //rowData = [
  //  { make: 'Toyota', model: 'Prius', price: '89891' },
  //  { make: 'Tesla', model: 'X', price: '10000' }
  //];

  constructor(
    private transactionService: TransactionService
  ) {
    this.rowData = [];
  }

  ngOnInit(): void {
    console.log('View component init');
    this.getTransactions();
    
  }

  getTransactions() {
    console.log('Getting Transactions...');
    this.transactionService.getTransactions().subscribe(
      (result) => {
        console.log(result);
        this.transactions = result;
        this.populateRowData();
      },
      (error) => console.log(error)
    );
  }

  populateRowData() {
    console.log('Populating');
    if (this.transactions) {
      console.log('Populating1');
      this.rowData = [];
      console.log('Populating2');
      this.transactions.forEach((value: Transaction) => {
        var date = new Date(value.transactionDate).toLocaleString();
        this.rowData.push(<TransactionRow>{ id: value.id, amount: value.amount, currency: value.currency, transactionDate: date, status: value.transactionStatus });
      });

      console.log('RowData:');
      console.log(this.rowData);
    }
  }
}

class TransactionRow {
  id: string;
  amount: number;
  currency: string;
  transactionDate: string;
  status: string;
}
