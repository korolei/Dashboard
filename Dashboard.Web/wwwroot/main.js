(window.webpackJsonp=window.webpackJsonp||[]).push([[5],{4:function(n,t,e){n.exports=e("zUnb")},F5nt:function(n,t,e){"use strict";e("AC1B"),e("lZOh"),e("6UzD"),e("BuZO"),e("q8iK"),e("bRuy"),e("PG31");var o=e("88/t"),l=e("t/Na"),a=(e("PCsq"),e("mrSG")),r=function(n){function t(t,e,o,l,a){var r=n.call(this)||this;return r.isSwaggerException=!0,r.message=t,r.status=e,r.response=o,r.headers=l,r.result=a,r}return Object(a.__extends)(t,n),t.isSwaggerException=function(n){return!0===n.isSwaggerException},t}(Error);e.d(t,"a",function(){return i});var i=function(){function n(n,t){this.http=n,this.configService=t,this.jsonParseReviver=void 0}return n.prototype.post=function(n,t){var e=this,a=t.replace(/[?&]$/,""),r={body:JSON.stringify(n),observe:"response",responseType:"blob",headers:new l.g({"Content-Type":"application/json",Accept:"*/*"})};return this.configService.loadingStateChanged.next(!0),this.http.request("post",a,r).flatMap(function(n){return e.processPost(n)}).catch(function(n){if(!(n instanceof l.i))return o.a.throw(n);try{return e.processPost(n)}catch(n){return o.a.throw(n)}})},n.prototype.processPost=function(n){var t=this,e=n.status,a=n instanceof l.h?n.body:n.error instanceof Blob?n.error:void 0;this.configService.loadingStateChanged.next(!1);var i={};if(n.headers)for(var c=0,p=n.headers.keys();c<p.length;c++){var s=p[c];i[s]=n.headers.get(s)}return 200===e?u(a).flatMap(function(n){var e;return e=""===n?null:JSON.parse(n,t.jsonParseReviver),o.a.of(e)}):200!==e&&204!==e?u(a).flatMap(function(n){return function(t,e,l,a,u){return o.a.throw(new r("An unexpected server error occurred.",e,n,i,null))}(0,e)}):o.a.of(null)},n}();function u(n){return new o.a(function(t){if(n){var e=new FileReader;e.onload=function(){t.next(this.result),t.complete()},e.readAsText(n)}else t.next(""),t.complete()})}},PCsq:function(n,t,e){"use strict";e.d(t,"a",function(){return l});var o=e("K9Ia"),l=(e("q8iK"),function(){function n(n){this.http=n,this.jsonParseReviver=void 0,this.loadingStateChanged=new o.a,this.globalConfigUrl="api/home/dashboard-settings",this.borrowingProfileConfigUrl="api/borrowingprofile/borrowing-profile-settings"}return n.prototype.loadGlobalConfigData=function(){var n=this;return this.loadConfigurationData(this.globalConfigUrl).then(function(t){n.globalConfigData=t})},n.prototype.loadBorrowingProfileConfigData=function(){var n=this;return this.loadConfigurationData(this.borrowingProfileConfigUrl).then(function(t){n.borrowingProfileConfigData=t})},n.prototype.loadConfigurationData=function(n){return this.http.get(n).toPromise().then(function(n){return n}).catch(function(n){return Promise.reject(n)})},Object.defineProperty(n.prototype,"globalConfig",{get:function(){return this.globalConfigData},enumerable:!0,configurable:!0}),Object.defineProperty(n.prototype,"borrowingProfileConfig",{get:function(){return this.borrowingProfileConfigData},enumerable:!0,configurable:!0}),n}())},crnd:function(n,t,e){var o={"./borrowing-profile/borrowing-profile.module.ngfactory":["qUBY",0]};function l(n){var t=o[n];return t?e.e(t[1]).then(function(){return e(t[0])}):Promise.resolve().then(function(){var t=new Error('Cannot find module "'+n+'".');throw t.code="MODULE_NOT_FOUND",t})}l.keys=function(){return Object.keys(o)},l.id="crnd",n.exports=l},zUnb:function(n,t,e){"use strict";e.r(t);var o=e("CcnG"),l=e("PCsq"),a=function(){},r=e("88/t"),i=(e("0GgQ"),e("HB89"),e("sgpt")),u=function(){function n(n,t,e){this.title=n,this.configService=t,this.spinnerService=e,this.isLoading=!1,this._initWinHeight=0}return n.prototype.ngOnInit=function(){var n=this;r.a.fromEvent(window,"resize").debounceTime(200).subscribe(function(t){return n._resizeFn(t)}),this.title.setTitle(this.configService.globalConfig.applicationName),this._initWinHeight=this.configService.globalConfig.canvasHeight,this._resizeFn(null),this.loadingSubs=this.configService.loadingStateChanged.subscribe(function(t){t?n.spinnerService.show():n.spinnerService.hide()})},n.prototype.ngOnDestroy=function(){this.loadingSubs.unsubscribe()},n.prototype.navToggledHandler=function(n){this.navOpen=n},n.prototype._resizeFn=function(n){this.minHeight=(n?n.target.innerHeight:this._initWinHeight)+"px"},n}(),c=e("4lDY"),p=e("u4HF"),s=e("aq8m"),d=e("qcfG"),g=e("xaNE"),f=e("FNNE"),m=e("gW6t"),h=e("ZYCi"),v=e("Ip0R"),C=function(){function n(){}return n.prototype.ngOnInit=function(){},n}(),_=o["\u0275crt"]({encapsulation:2,styles:[],data:{}});function b(n){return o["\u0275vid"](0,[(n()(),o["\u0275eld"](0,0,null,null,9,"nav",[["class","nav"],["id","nav"],["role","navigation"]],null,null,null,null,null)),(n()(),o["\u0275eld"](1,0,null,null,8,"ul",[["class","nav-list"]],null,null,null,null,null)),(n()(),o["\u0275eld"](2,0,null,null,7,"li",[],null,null,null,null,null)),(n()(),o["\u0275eld"](3,0,null,null,6,"a",[["routerLink","/borrowing-profile"],["routerLinkActive","active"]],[[1,"target",0],[8,"href",4]],[[null,"click"]],function(n,t,e){var l=!0;return"click"===t&&(l=!1!==o["\u0275nov"](n,4).onClick(e.button,e.ctrlKey,e.metaKey,e.shiftKey)&&l),l},null,null)),o["\u0275did"](4,671744,[[2,4]],0,h.m,[h.k,h.a,v.LocationStrategy],{routerLink:[0,"routerLink"]},null),o["\u0275did"](5,1720320,null,2,h.l,[h.k,o.ElementRef,o.Renderer2,o.ChangeDetectorRef],{routerLinkActiveOptions:[0,"routerLinkActiveOptions"],routerLinkActive:[1,"routerLinkActive"]},null),o["\u0275qud"](603979776,1,{links:1}),o["\u0275qud"](603979776,2,{linksWithHrefs:1}),o["\u0275pod"](8,{exact:0}),(n()(),o["\u0275ted"](-1,null,["Borrowing Profile"]))],function(n,t){n(t,4,0,"/borrowing-profile"),n(t,5,0,n(t,8,0,!0),"active")},function(n,t){n(t,3,0,o["\u0275nov"](t,4).target,o["\u0275nov"](t,4).href)})}var O=o["\u0275ccf"]("app-dashboard-items",C,function(n){return o["\u0275vid"](0,[(n()(),o["\u0275eld"](0,0,null,null,1,"app-dashboard-items",[],null,null,null,b,_)),o["\u0275did"](1,114688,null,0,C,[],null,null)],function(n,t){n(t,1,0)},null)},{},{},[]),P=e("Y/BS"),M=(e("O5R2"),function(){function n(n){this.configServices=n}return n.prototype.ngOnInit=function(){this.appTitle=this.configServices.globalConfig.applicationName},n}()),y=o["\u0275crt"]({encapsulation:0,styles:[[".nav[_ngcontent-%COMP%]{background:#eee;backface-visibility:hidden;-webkit-backface-visibility:hidden;box-shadow:inset -8px 0 8px -6px rgba(0,0,0,.2);display:none;height:100%;overflow-y:auto;padding:3%;position:absolute;top:0;-webkit-transform:translate3d(-100%,0,0);transform:translate3d(-100%,0,0);width:270px}.nav-closed[_nghost-%COMP%]   .nav[_ngcontent-%COMP%], .nav-closed   [_nghost-%COMP%]   .nav[_ngcontent-%COMP%], .nav-open[_nghost-%COMP%]   .nav[_ngcontent-%COMP%], .nav-open   [_nghost-%COMP%]   .nav[_ngcontent-%COMP%]{display:block}.nav[_ngcontent-%COMP%]   .active[_ngcontent-%COMP%]{font-weight:700}.nav-list[_ngcontent-%COMP%]{list-style:none;margin-bottom:0;padding-left:0}.nav-list[_ngcontent-%COMP%]   a[_ngcontent-%COMP%]{display:block;padding:6px}.nav-list[_ngcontent-%COMP%]   a[_ngcontent-%COMP%]:active, .nav-list[_ngcontent-%COMP%]   a[_ngcontent-%COMP%]:focus, .nav-list[_ngcontent-%COMP%]   a[_ngcontent-%COMP%]:hover{text-decoration:none}.toggle-offcanvas[_ngcontent-%COMP%]{border-right:1px solid rgba(255,255,255,.5);display:inline-block;height:50px;padding:23.5px 13px;position:relative;text-align:center;width:50px;z-index:100}.toggle-offcanvas[_ngcontent-%COMP%]   span[_ngcontent-%COMP%], .toggle-offcanvas[_ngcontent-%COMP%]   span[_ngcontent-%COMP%]:after, .toggle-offcanvas[_ngcontent-%COMP%]   span[_ngcontent-%COMP%]:before{background:#fff;border-radius:1px;content:'';display:block;height:3px;position:absolute;transition:all 250ms ease-in-out;width:24px}.toggle-offcanvas[_ngcontent-%COMP%]   span[_ngcontent-%COMP%]:before{top:-9px}.toggle-offcanvas[_ngcontent-%COMP%]   span[_ngcontent-%COMP%]:after{bottom:-9px}.nav-open[_nghost-%COMP%]   .toggle-offcanvas[_ngcontent-%COMP%]   span[_ngcontent-%COMP%], .nav-open   [_nghost-%COMP%]   .toggle-offcanvas[_ngcontent-%COMP%]   span[_ngcontent-%COMP%]{background:0 0}.nav-open[_nghost-%COMP%]   .toggle-offcanvas[_ngcontent-%COMP%]   span[_ngcontent-%COMP%]:after, .nav-open   [_nghost-%COMP%]   .toggle-offcanvas[_ngcontent-%COMP%]   span[_ngcontent-%COMP%]:after, .nav-open[_nghost-%COMP%]   .toggle-offcanvas[_ngcontent-%COMP%]   span[_ngcontent-%COMP%]:before, .nav-open   [_nghost-%COMP%]   .toggle-offcanvas[_ngcontent-%COMP%]   span[_ngcontent-%COMP%]:before{top:0}.nav-open[_nghost-%COMP%]   .toggle-offcanvas[_ngcontent-%COMP%]   span[_ngcontent-%COMP%]:before, .nav-open   [_nghost-%COMP%]   .toggle-offcanvas[_ngcontent-%COMP%]   span[_ngcontent-%COMP%]:before{-webkit-transform:rotate(45deg);transform:rotate(45deg)}.nav-open[_nghost-%COMP%]   .toggle-offcanvas[_ngcontent-%COMP%]   span[_ngcontent-%COMP%]:after, .nav-open   [_nghost-%COMP%]   .toggle-offcanvas[_ngcontent-%COMP%]   span[_ngcontent-%COMP%]:after{-webkit-transform:rotate(-45deg);transform:rotate(-45deg)}.header-page[_ngcontent-%COMP%]{color:#fff;height:50px;margin-bottom:10px;position:relative}.header-page-siteTitle[_ngcontent-%COMP%]{font-size:30px;line-height:50px;margin:0;padding:0 0 0 60px;position:absolute;top:0;width:100%}.header-page[_ngcontent-%COMP%]   a[_ngcontent-%COMP%]{color:#fff;text-decoration:none}"]],data:{}});function k(n){return o["\u0275vid"](0,[(n()(),o["\u0275eld"](0,0,null,null,5,"header",[["class","header"],["id","header"]],null,null,null,null,null)),(n()(),o["\u0275eld"](1,0,null,null,4,"div",[["class","header-page bg-primary"]],null,null,null,null,null)),(n()(),o["\u0275eld"](2,0,null,null,3,"h1",[["class","header-page-siteTitle"]],null,null,null,null,null)),(n()(),o["\u0275eld"](3,0,null,null,2,"a",[["routerLink","/"]],[[1,"target",0],[8,"href",4]],[[null,"click"]],function(n,t,e){var l=!0;return"click"===t&&(l=!1!==o["\u0275nov"](n,4).onClick(e.button,e.ctrlKey,e.metaKey,e.shiftKey)&&l),l},null,null)),o["\u0275did"](4,671744,null,0,h.m,[h.k,h.a,v.LocationStrategy],{routerLink:[0,"routerLink"]},null),(n()(),o["\u0275ted"](5,null,["",""]))],function(n,t){n(t,4,0,"/")},function(n,t){var e=t.component;n(t,3,0,o["\u0275nov"](t,4).target,o["\u0275nov"](t,4).href),n(t,5,0,e.appTitle)})}var w=function(){function n(){}return n.prototype.ngOnInit=function(){},n}(),N=o["\u0275crt"]({encapsulation:0,styles:[["[_nghost-%COMP%]{display:block;padding-bottom:10px}p[_ngcontent-%COMP%]{font-size:12px;margin-bottom:0}"]],data:{}});function L(n){return o["\u0275vid"](0,[(n()(),o["\u0275eld"](0,0,null,null,1,"p",[["class","text-center"]],null,null,null,null,null)),(n()(),o["\u0275ted"](-1,null,[" OFA Dashboard "]))],null,null)}var S=e("ZYjt"),D=o["\u0275crt"]({encapsulation:2,styles:[],data:{}});function x(n){return o["\u0275vid"](0,[(n()(),o["\u0275eld"](0,0,null,null,12,"div",[["class","layout-overflow"]],null,null,null,null,null)),(n()(),o["\u0275eld"](1,0,null,null,11,"div",[["class","layout-canvas"]],[[4,"min-height",null]],null,null,null,null)),o["\u0275did"](2,278528,null,0,v.NgClass,[o.IterableDiffers,o.KeyValueDiffers,o.ElementRef,o.Renderer2],{klass:[0,"klass"],ngClass:[1,"ngClass"]},null),o["\u0275pod"](3,{"nav-open":0,"nav-closed":1}),(n()(),o["\u0275eld"](4,0,null,null,1,"ng4-loading-spinner",[],null,null,null,P.b,P.a)),o["\u0275did"](5,180224,null,0,i.Ng4LoadingSpinnerComponent,[i.Ng4LoadingSpinnerService],null,null),(n()(),o["\u0275eld"](6,0,null,null,1,"app-header",[],null,null,null,k,y)),o["\u0275did"](7,114688,null,0,M,[l.a],null,null),(n()(),o["\u0275eld"](8,0,null,null,2,"div",[["class","container-fluid"]],null,null,null,null,null)),(n()(),o["\u0275eld"](9,16777216,null,null,1,"router-outlet",[],null,null,null,null,null)),o["\u0275did"](10,212992,null,0,h.o,[h.b,o.ViewContainerRef,o.ComponentFactoryResolver,[8,null],o.ChangeDetectorRef],null,null),(n()(),o["\u0275eld"](11,0,null,null,1,"app-footer",[],null,null,null,L,N)),o["\u0275did"](12,114688,null,0,w,[],null,null)],function(n,t){var e=t.component;n(t,2,0,"layout-canvas",n(t,3,0,e.navOpen,!e.navOpen)),n(t,7,0),n(t,10,0),n(t,12,0)},function(n,t){n(t,1,0,t.component.minHeight)})}var R=o["\u0275ccf"]("app-root",u,function(n){return o["\u0275vid"](0,[(n()(),o["\u0275eld"](0,0,null,null,1,"app-root",[],null,null,null,x,D)),o["\u0275did"](1,245760,null,0,u,[S.h,l.a,i.Ng4LoadingSpinnerService],null,null)],function(n,t){n(t,1,0)},null)},{},{},[]),T=e("gIcY"),A=e("Ovjw"),E=e("iCtU"),I=e("e5OV"),F=e("8NoF"),j=e("FeSO"),U=e("ysnj"),q=e("5sLU"),z=e("oYRQ"),H=e("q7oS"),K=e("OU4G"),Z=e("bSlz"),B=e("9n00"),G=e("Wqpw"),Y=e("Ok6J"),W=e("Ilhw"),J=e("ebCm"),V=e("NGEN"),X=e("ejuw"),Q=e("swaV"),$=e("Zt+D"),nn=e("lMb6"),tn=e("t/Na"),en=e("F/XL"),on=function(){function n(){}return n.prototype.preload=function(n,t){return n.data&&n.data.preload?t():Object(en.a)(null)},n}(),ln=e("F5nt"),an=e("bt6x"),rn=e("0XGt"),un=e("nhl2"),cn=e("gpiN"),pn=e("MVL9"),sn=e("j2fZ"),dn=e("LKjY"),gn=e("PsaP"),fn=e("InZo"),mn=e("C9m0"),hn=e("+NDo"),vn=e("4WQT"),Cn=e("wtSO"),_n=e("NlYj"),bn=e("neuq"),On=e("y+WT"),Pn=e("eUd/"),Mn=function(){},yn=o["\u0275cmf"](a,[u],function(n){return o["\u0275mod"]([o["\u0275mpd"](512,o.ComponentFactoryResolver,o["\u0275CodegenComponentFactoryResolver"],[[8,[c.a,p.a,s.a,d.a,g.a,f.a,m.a,O,R]],[3,o.ComponentFactoryResolver],o.NgModuleRef]),o["\u0275mpd"](5120,o.LOCALE_ID,o["\u0275angular_packages_core_core_l"],[[3,o.LOCALE_ID]]),o["\u0275mpd"](4608,v.NgLocalization,v.NgLocaleLocalization,[o.LOCALE_ID,[2,v["\u0275angular_packages_common_common_a"]]]),o["\u0275mpd"](5120,o.APP_ID,o["\u0275angular_packages_core_core_f"],[]),o["\u0275mpd"](5120,o.IterableDiffers,o["\u0275angular_packages_core_core_j"],[]),o["\u0275mpd"](5120,o.KeyValueDiffers,o["\u0275angular_packages_core_core_k"],[]),o["\u0275mpd"](4608,S.b,S.q,[v.DOCUMENT]),o["\u0275mpd"](6144,o.Sanitizer,null,[S.b]),o["\u0275mpd"](4608,S.e,S.f,[]),o["\u0275mpd"](5120,S.c,function(n,t,e,o,l,a,r){return[new S.j(n,t,e),new S.n(o),new S.m(l,a,r)]},[v.DOCUMENT,o.NgZone,[2,o.PLATFORM_ID],v.DOCUMENT,v.DOCUMENT,S.e,o["\u0275Console"]]),o["\u0275mpd"](4608,S.d,S.d,[S.c,o.NgZone]),o["\u0275mpd"](135680,S.l,S.l,[v.DOCUMENT]),o["\u0275mpd"](4608,S.k,S.k,[S.d,S.l]),o["\u0275mpd"](6144,o.RendererFactory2,null,[S.k]),o["\u0275mpd"](6144,S.o,null,[S.l]),o["\u0275mpd"](4608,o.Testability,o.Testability,[o.NgZone]),o["\u0275mpd"](4608,S.g,S.g,[v.DOCUMENT]),o["\u0275mpd"](4608,S.h,S.h,[v.DOCUMENT]),o["\u0275mpd"](4608,T["\u0275angular_packages_forms_forms_i"],T["\u0275angular_packages_forms_forms_i"],[]),o["\u0275mpd"](4608,A.a,A.a,[o.ApplicationRef,o.Injector,o.ComponentFactoryResolver,v.DOCUMENT]),o["\u0275mpd"](4608,E.a,E.a,[o.ComponentFactoryResolver,o.Injector,A.a]),o["\u0275mpd"](4608,I.a,I.a,[]),o["\u0275mpd"](4608,F.a,F.a,[]),o["\u0275mpd"](4608,j.a,j.a,[]),o["\u0275mpd"](135680,U.c,U.c,[v.DOCUMENT,U.a]),o["\u0275mpd"](4608,q.a,q.a,[]),o["\u0275mpd"](4608,z.a,z.a,[]),o["\u0275mpd"](4608,H.a,H.a,[]),o["\u0275mpd"](4608,K.a,K.b,[]),o["\u0275mpd"](4608,v.DatePipe,v.DatePipe,[o.LOCALE_ID]),o["\u0275mpd"](4608,Z.a,Z.b,[o.LOCALE_ID,v.DatePipe]),o["\u0275mpd"](4608,B.b,B.a,[]),o["\u0275mpd"](4608,G.a,G.b,[]),o["\u0275mpd"](4608,Y.a,Y.a,[]),o["\u0275mpd"](4608,W.a,W.a,[v.DOCUMENT,o.NgZone]),o["\u0275mpd"](4608,J.a,J.a,[]),o["\u0275mpd"](4608,V.a,V.a,[]),o["\u0275mpd"](4608,X.a,X.a,[]),o["\u0275mpd"](4608,Q.a,Q.a,[]),o["\u0275mpd"](4608,$.a,$.a,[]),o["\u0275mpd"](4608,nn.a,nn.a,[]),o["\u0275mpd"](4608,i.Ng4LoadingSpinnerService,i.Ng4LoadingSpinnerService,[]),o["\u0275mpd"](4608,T.FormBuilder,T.FormBuilder,[]),o["\u0275mpd"](4608,tn.k,tn.q,[v.DOCUMENT,o.PLATFORM_ID,tn.o]),o["\u0275mpd"](4608,tn.r,tn.r,[tn.k,tn.p]),o["\u0275mpd"](5120,tn.a,function(n){return[n]},[tn.r]),o["\u0275mpd"](5120,h.a,h.x,[h.k]),o["\u0275mpd"](4608,h.d,h.d,[]),o["\u0275mpd"](6144,h.f,null,[h.d]),o["\u0275mpd"](135680,h.p,h.p,[h.k,o.NgModuleFactoryLoader,o.Compiler,o.Injector,h.f]),o["\u0275mpd"](4608,h.e,h.e,[]),o["\u0275mpd"](5120,h.h,h.A,[h.y]),o["\u0275mpd"](5120,o.APP_BOOTSTRAP_LISTENER,function(n){return[n]},[h.h]),o["\u0275mpd"](4608,on,on,[]),o["\u0275mpd"](4608,ln.a,ln.a,[tn.c,l.a]),o["\u0275mpd"](1073742336,v.CommonModule,v.CommonModule,[]),o["\u0275mpd"](1024,o.ErrorHandler,S.p,[]),o["\u0275mpd"](1024,o.NgProbeToken,function(){return[h.t()]},[]),o["\u0275mpd"](512,h.y,h.y,[o.Injector]),o["\u0275mpd"](512,tn.n,tn.n,[]),o["\u0275mpd"](2048,tn.l,null,[tn.n]),o["\u0275mpd"](512,tn.j,tn.j,[tn.l]),o["\u0275mpd"](2048,tn.b,null,[tn.j]),o["\u0275mpd"](512,tn.f,tn.m,[tn.b,o.Injector]),o["\u0275mpd"](512,tn.c,tn.c,[tn.f]),o["\u0275mpd"](512,l.a,l.a,[tn.c]),o["\u0275mpd"](1024,o.APP_INITIALIZER,function(n,t,e){return[S.r(n),h.z(t),(o=e,function(){return o.loadGlobalConfigData()})];var o},[[2,o.NgProbeToken],h.y,l.a]),o["\u0275mpd"](512,o.ApplicationInitStatus,o.ApplicationInitStatus,[[2,o.APP_INITIALIZER]]),o["\u0275mpd"](131584,o.ApplicationRef,o.ApplicationRef,[o.NgZone,o["\u0275Console"],o.Injector,o.ErrorHandler,o.ComponentFactoryResolver,o.ApplicationInitStatus]),o["\u0275mpd"](1073742336,o.ApplicationModule,o.ApplicationModule,[o.ApplicationRef]),o["\u0275mpd"](1073742336,S.a,S.a,[[3,S.a]]),o["\u0275mpd"](1073742336,an.a,an.a,[]),o["\u0275mpd"](1073742336,rn.a,rn.a,[]),o["\u0275mpd"](1073742336,un.a,un.a,[]),o["\u0275mpd"](1073742336,cn.a,cn.a,[]),o["\u0275mpd"](1073742336,pn.a,pn.a,[]),o["\u0275mpd"](1073742336,sn.a,sn.a,[]),o["\u0275mpd"](1073742336,dn.a,dn.a,[]),o["\u0275mpd"](1073742336,gn.a,gn.a,[]),o["\u0275mpd"](1073742336,T["\u0275angular_packages_forms_forms_bb"],T["\u0275angular_packages_forms_forms_bb"],[]),o["\u0275mpd"](1073742336,T.FormsModule,T.FormsModule,[]),o["\u0275mpd"](1073742336,fn.a,fn.a,[]),o["\u0275mpd"](1073742336,mn.a,mn.a,[]),o["\u0275mpd"](1073742336,hn.a,hn.a,[]),o["\u0275mpd"](1073742336,vn.a,vn.a,[]),o["\u0275mpd"](1073742336,Cn.a,Cn.a,[]),o["\u0275mpd"](1073742336,_n.a,_n.a,[]),o["\u0275mpd"](1073742336,bn.a,bn.a,[]),o["\u0275mpd"](1073742336,On.a,On.a,[]),o["\u0275mpd"](1073742336,Pn.a,Pn.a,[]),o["\u0275mpd"](1073742336,i.Ng4LoadingSpinnerModule,i.Ng4LoadingSpinnerModule,[]),o["\u0275mpd"](1073742336,T.ReactiveFormsModule,T.ReactiveFormsModule,[]),o["\u0275mpd"](1073742336,tn.e,tn.e,[]),o["\u0275mpd"](1073742336,tn.d,tn.d,[]),o["\u0275mpd"](1024,h.s,h.v,[[3,h.k]]),o["\u0275mpd"](512,h.r,h.c,[]),o["\u0275mpd"](512,h.b,h.b,[]),o["\u0275mpd"](256,h.g,{useHash:!0},[]),o["\u0275mpd"](1024,v.LocationStrategy,h.u,[v.PlatformLocation,[2,v.APP_BASE_HREF],h.g]),o["\u0275mpd"](512,v.Location,v.Location,[v.LocationStrategy]),o["\u0275mpd"](512,o.Compiler,o.Compiler,[]),o["\u0275mpd"](512,o.NgModuleFactoryLoader,o.SystemJsNgModuleLoader,[o.Compiler,[2,o.SystemJsNgModuleLoaderConfig]]),o["\u0275mpd"](1024,h.i,function(){return[[{path:"",component:C},{path:"borrowing-profile",loadChildren:"./borrowing-profile/borrowing-profile.module#BorrowingProfileModule"},{path:"**",redirectTo:"app-dashboard-items"}]]},[]),o["\u0275mpd"](1024,h.k,h.w,[o.ApplicationRef,h.r,h.b,v.Location,o.Injector,o.NgModuleFactoryLoader,o.Compiler,h.i,h.g,[2,h.q],[2,h.j]]),o["\u0275mpd"](1073742336,h.n,h.n,[[2,h.s],[2,h.k]]),o["\u0275mpd"](1073742336,Mn,Mn,[]),o["\u0275mpd"](1073742336,a,a,[]),o["\u0275mpd"](256,o["\u0275APP_ROOT"],!0,[]),o["\u0275mpd"](256,U.a,U.b,[]),o["\u0275mpd"](256,tn.o,"XSRF-TOKEN",[]),o["\u0275mpd"](256,tn.p,"X-XSRF-TOKEN",[])])});Object(o.enableProdMode)(),S.i().bootstrapModuleFactory(yn).catch(function(n){return console.log(n)})}},[[4,1,2]]]);