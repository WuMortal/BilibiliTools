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

    public queryEpisode(data: any): Observable<PageList<Episode>> {
        return this.apiHttp.Post(`/api/Data/QueryEpisodes`, data);
    }

    public getAllUploader(): Observable<Uploader[]> {
        return this.apiHttp.Get(`/api/Data/GetAllUploader`);
    }

}

export class Episode {
    public episodeId!: string;
    public episodeTitle!: string;
    public coverImagePath!: string;
    public coverUrl!: string;
    public createDate!: string;
}

export class Uploader {
    public uploaderId!: string;
    public uploaderName!: string;
    public uploaderAvatarUrl!: string;
    public uploadCount!: number;
    public createDate!: string;
}

class PageList<T> {
    public totalCount!: number;
    public item!: Array<T>;
}
