import { HttpClient, HttpHeaders, HttpParams, HttpRequest, HttpResponse } from '@angular/common/http';
import { inject, Injectable, model, ModelSignal, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';
import { of, tap } from 'rxjs';
import { Photo } from '../_models/photo';
import { PaginatedResult } from '../_models/pagination';
import { UserParams } from '../_models/userParam';
import { AccountService } from './account.service';
import { setPaginationHeaders, setPaginationResponse,  } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class MembersService {

  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  private accountService = inject(AccountService);
  members = signal<Member[]>([]);
  paginationResult = signal<PaginatedResult<Member[]> | null>(null);
  memberCache = new Map();
  user = this.accountService.currentUser();
  userParamss = signal<UserParams>(new UserParams(this.user));
 
resetParams(){
  this.userParamss.set(new UserParams(this.user));
}

  getMembers() {
    const response = this.memberCache.get(Object.values(this.userParamss()).join('-'));
    if (response) return setPaginationResponse(response , this.paginationResult);

    let params = setPaginationHeaders(this.userParamss().pageNumber, this.userParamss().pageSize);
    params = params.append('gender', this.userParamss().gender);
    params = params.append('minAge', this.userParamss().minAge);
    params = params.append('maxAge', this.userParamss().maxAge);
    params = params.append('orderBy', this.userParamss().orderBy);

    return this.http.get<Member[]>(this.baseUrl + 'users', { observe: 'response', params }).subscribe(
      {
        next: response => {
          setPaginationResponse(response ,this.paginationResult);
          this.memberCache.set(Object.values(this.userParamss()).join('-'), response);
        }
      });
  }



  getMember(username: string) {
    // Older Approach
    // const member = this.members().find(x => x.username == username);
    // if (member != undefined) return of(member);

    const member: Member = [...this.memberCache.values()]
      .reduce((arr, e) => arr.concat(e.body), [])
      .find((m: Member) => m.username == username);
    if (member) return of(member);
    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users/', member).pipe(
      // tap(() => {
      //   this.members.update(x => x.map(m => m.username == member.username ? member : m))
      // })
    );
  }

  setMainPhoto(photo: Photo) {
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photo.id, {}).pipe(
      // tap(() => {
      //   this.members.update(members => members.map(m => {
      //     if (m.photos.includes(photo)) {
      //       m.photoUrl = photo.url
      //     }
      //     return m;
      //   }))
      // })
    );
  }

  deletePhoto(photo: Photo) {
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photo.id).pipe(
      // tap(() => {
      //   this.members.update(member => member.map(m => {
      //     if (m.photos.includes(photo)) {
      //       m.photos.filter(x => x.id != photo.id)
      //     }
      //     return m
      //   }))
      // })
    );
  }
}
