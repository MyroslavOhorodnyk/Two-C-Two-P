export interface Transaction {
  id: string;
  amount: number;
  currency: string;
  transactionStatus: string;
  transactionDate: Date;
}
