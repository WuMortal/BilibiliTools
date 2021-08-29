import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ApiHttp } from './ApiHttp';

@Injectable({
    providedIn: 'root'
})
export class SystemApi {

    constructor(
        private apiHttp: ApiHttp
    ) { ApiHttp.serverAddress = environment.serverAddress; }

    public runAnalyze(): Observable<number> {
        return this.apiHttp.Post(`/api/System/RunAnalyze`, null);
    }

}
