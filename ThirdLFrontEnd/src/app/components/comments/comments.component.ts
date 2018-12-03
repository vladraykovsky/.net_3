import { Component, OnInit, Input } from '@angular/core';
import {CommentService} from '../../services/comment.service';
import {Comment} from '../../models/Comment';
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})
export class CommentsComponent implements OnInit {

  patientId =0;
  commentValue = '';
  @Input() comments
  constructor(
    private activatedRouter: ActivatedRoute,
    private commentService: CommentService) { }

  ngOnInit() {
    this.activatedRouter.params.subscribe( params => {
      this.patientId = params['id'];
    });
  }

  removeComment(comment: Comment) {
    this.commentService.deletePatient(this.patientId, comment.id)
      .subscribe( () => {
      this.comments = this.comments.filter( it => it.id !== comment.id);
    });
  }

  addComment() {
    this.commentService.saveComment(this.patientId , { id:0 , value: `${this.commentValue}`,firstName: localStorage.getItem("firstName"), lastName: localStorage.getItem("lastName") , creationData: new Date().toDateString() })
      .subscribe( commentReceived => {
        this.comments.push(commentReceived);
        this.commentValue = '';
      });
  }

}
