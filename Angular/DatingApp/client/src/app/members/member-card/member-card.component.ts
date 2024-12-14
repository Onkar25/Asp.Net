import { Component, computed, inject, input } from '@angular/core';
import { Member } from '../../_models/member';
import { RouterLink } from '@angular/router';
import { LikesService } from '../../_services/likes.service';

@Component({
  selector: 'app-member-card',
  imports: [RouterLink],
  templateUrl: './member-card.component.html',
  styleUrl: './member-card.component.css'
})
export class MemberCardComponent {
  member = input.required<Member>();

  private likeService = inject(LikesService);
  hasLiked = computed(() => this.likeService.likeIds().includes(this.member().id));


  toggleLikes() {
    this.likeService.toggleLike(this.member().id).subscribe({
      next: () => {
        if (this.hasLiked()) {
          this.likeService.likeIds.update(id => id.filter(x => x != this.member().id))
        } else {
          this.likeService.likeIds.update(id => [...id, this.member().id])
        }
      }
    })
  }
}
