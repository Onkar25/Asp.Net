import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Member } from '../_models/member';
import { PaginatedResult } from '../_models/pagination';
import { setPaginationHeaders, setPaginationResponse } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class LikesService {

  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  likeIds = signal<number[]>([]);
  paginationResult = signal<PaginatedResult<Member[]> | null>(null);

  toggleLike(targerId: number) {
    return this.http.post(`${this.baseUrl}likes/${targerId}`, {});
  }

  getLikes(predicates: string, pageNumber: number, pageSize: number) {
    let params = setPaginationHeaders(pageNumber, pageSize);
    params = params.append('predicate', predicates);
    return this.http.get<Member[]>(`${this.baseUrl}likes`, 
      { observe: 'response', params }).subscribe({
      next: response => setPaginationResponse(response, this.paginationResult)
    });
  }


  getLikeIds() {
    return this.http.get<number[]>(`${this.baseUrl}likes/list`).subscribe({
      next: ids => this.likeIds.set(ids)
    });
  }
}
