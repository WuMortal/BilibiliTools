import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiHttp {

  public static serverAddress: string;

  constructor(
    private http: HttpClient,
  ) { }

  public Get<T>(address: string): Observable<T> {
    return this.http.get<T>(`${ApiHttp.serverAddress}${address}`, {
    }).pipe(catchError(this.handleError));
  }

  public Post<T>(address: string, data: any): Observable<T> {
    return this.http.post<T>(`${ApiHttp.serverAddress}${address}`, data, {
      withCredentials: true
    }).pipe(catchError(this.handleError));
  }

  private handleError(error: any): Promise<any> {
    console.error(error);
    return Promise.reject(error.message || error);
  }
}
