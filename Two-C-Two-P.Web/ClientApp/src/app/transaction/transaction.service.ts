import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Transaction } from './models/transaction.model';
@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  apiServiceBaseUrl: string;

  constructor(@Inject('BASE_URL') baseUrl: string,
    private http: HttpClient) {
    this.apiServiceBaseUrl = baseUrl + 'api/transactions/';
  }

  getTransactions() {
    return this.http.get<Transaction[]>(this.apiServiceBaseUrl);
  }
}
