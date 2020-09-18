import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";

import { AppComponent } from "./app.component";
import { NavMenuComponent } from "./nav-menu/nav-menu.component";
import { HomeComponent } from "./home/home.component";
import { LoggingComponent } from "./components/logging/logging.component";
import { LanguageAdminComponent } from "./components/languageAdmin/languageAdmin.component";
import { ProfileComponent } from "./components/profile/profile.component";
import { TopicComponent } from "./components/topic/topic.component";
import { TopicSearchComponent } from "./components/topic/topicSearch.component";
import { TopicDiscoveryComponent } from "./components/topic/discovery.component";
import { MentorshipComponent } from "./components/mentorship/mentorship.component";
import { ProfileLink } from "./components/profile/profileLink.component";
import { TopicLink } from "./components/topic/topicLink.component";
import { MentorList } from "./components/mentorship/mentorList.component";
import { AdminHome } from "./components/admin/adminHome.component";
import { SupportComponent } from "./components/support/support.component";
import { TileComponent } from "./components/tile/tile.component";
import { LinkAdminComponent } from "./components/linkAdmin/linkAdmin.component";
import { DashboardAdminComponent } from "./components/dashboardAdmin/dashboardAdmin.component";
import { UserAdminComponent } from "./components/userAdmin/userAdmin.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AdminHome,
    LoggingComponent,
    LanguageAdminComponent,
    ProfileComponent,
    TopicComponent,
    TopicDiscoveryComponent,
    TopicSearchComponent,
    MentorshipComponent,
    ProfileLink,
    TopicLink,
    MentorList,
    SupportComponent,
    TileComponent,
    LinkAdminComponent,
    DashboardAdminComponent,
    UserAdminComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: "", component: HomeComponent, pathMatch: "full" },
      {
        path: "admin", component: AdminHome, children: [
          { path: "", component: DashboardAdminComponent, pathMatch: "full" },
          { path: "dashboard", component: DashboardAdminComponent },
          { path: "language", component: LanguageAdminComponent },
          { path: "logging", component: LoggingComponent },
          { path: "link", component: LinkAdminComponent },
          { path: "user", component: UserAdminComponent }
        ]
      },
      { path: "profile", component: ProfileComponent },
      { path: "profile/:username", component: ProfileComponent },
      { path: "discovery", component: TopicDiscoveryComponent },
      { path: "topic/create/:name", component: TopicComponent },
      { path: "topic/:topicId", component: TopicComponent },
      { path: "mentorship/:mentorshipId", component: MentorshipComponent },
      { path: "support", component: SupportComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
