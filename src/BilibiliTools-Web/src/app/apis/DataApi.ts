import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ApiHttp } from './ApiHttp';

@Injectable({
    providedIn: 'root'
})
export class DataApi {

    constructor(
        private apiHttp: ApiHttp
    ) { ApiHttp.serverAddress = environment.serverAddress; }

    public getEpisode(): Observable<Episode[]> {
        return this.apiHttp.Get(`/api/Data/Episodes`);
    }


}

export class Episode {
    public episodeId!: string;
    public episodeTitle!: string;
    public coverImagePath!: string;
    public coverUrl!: string;
    public createDate!: string;
}

