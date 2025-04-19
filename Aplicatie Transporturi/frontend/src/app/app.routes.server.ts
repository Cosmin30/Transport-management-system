import { RenderMode, ServerRoute } from '@angular/ssr';

export const serverRoutes: ServerRoute[] = [
  {
    path: '', // doar pagina de start
    renderMode: RenderMode.Prerender,
  }
];
