import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Customers } from '../models/customers';
import { Observable } from 'rxjs';

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
}
