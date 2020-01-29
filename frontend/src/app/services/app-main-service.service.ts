import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Customers } from '../models/customers';
import { Observable } from 'rxjs';
import { Alterations } from '../models/alterations';

@Injectable({
  providedIn: 'root'
})
export class AppMainServiceService {

  constructor(private httpClient: HttpClient) { }

  getCustomers(): Observable<Customers[]> {
    return this.httpClient.get<Customers[]>(`${environment.apiUrl}api/CustomersService/Customers`);
  }

  getCustomerById(id: number): Observable<Customers> {
    return this.httpClient.get<Customers>(`${environment.apiUrl}api/CustomersService/Customers/${id}`);
  }

  getAlterationsByCustomerId(customerId: number): Observable<Alterations[]> {
    return this.httpClient.get<Alterations[]>(`${environment.apiUrl}api/AlteringsService/Alterings?customerId=${customerId}`);
  }
}
