import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Customers } from '../models/customers';
import { Observable } from 'rxjs';
import { Alterations } from '../models/alterations';
import { NewPayment } from '../models/new-payment';
import { NewAlteration } from '../models/new-alteration';

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

  registerNewPayment(model: NewPayment): Observable<string> {
    return this.httpClient.post<string>(`${environment.apiUrl}api/PaymentsService/Payments`,
      model
    );
  }

  createAlteration(model: NewAlteration): Observable<string> {
    return this.httpClient.post<string>(`${environment.apiUrl}api/AlteringsService/Alterings`,
      model
    );
  }

  setOnGoing(alterationId: number): Observable<string> {
    return this.httpClient.post<string>(`${environment.apiUrl}api/AlteringsService/Alterings/SetOnGoing?alterationId=${alterationId}`,
      {}
    );
  }

  setDone(alterationId: number): Observable<string> {
    return this.httpClient.post<string>(`${environment.apiUrl}api/AlteringsService/Alterings/SetDone?alterationId=${alterationId}`,
      {}
    );
  }
}
