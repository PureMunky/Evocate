import { Component } from "@angular/core";
import { ApiService } from "../../services/api.service";
import { UserService } from "../../services/user.service";
import { Link } from "../../models/link";

@Component({
  selector: "link-admin",
  templateUrl: "./linkAdmin.component.html"
})
export class LinkAdminComponent {
  public Links: Array<Link> = [];
  public AllLinks: Array<Link> = [];

  constructor(
    private apiService: ApiService,
    private userService: UserService
  ) {
    this.loadLinks();
  }

  public loadLinks() {
    return this.apiService.get("V1/Link/Get")
      .then(data => {
        this.AllLinks = data;
        this.filterAll();
      });
  };

  public saveLink(link: Link) {
    return this.apiService.post("V1/Link/Post", link);
  }

  public filterAll() {
    this.Links = this.AllLinks;
  }

  public filterUnreviewed() {
    this.Links = this.AllLinks.filter(l => !l.reviewed);
  }

  public filterActive() {
    this.Links = this.AllLinks.filter(l => l.active);
  }

}
