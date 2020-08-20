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
    MentorList
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: "", component: HomeComponent, pathMatch: "full" },
      {
        path: "admin", component: AdminHome, children: [
          { path: "language", component: LanguageAdminComponent },
          { path: "logging", component: LoggingComponent },
        ]
      },
      { path: "profile/:username", component: ProfileComponent },
      { path: "discovery", component: TopicDiscoveryComponent },
      { path: "topic/create/:name", component: TopicComponent },
      { path: "topic/:topicId", component: TopicComponent },
      { path: "mentorship/:mentorshipId", component: MentorshipComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
