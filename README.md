# Trackly

Trackly is a full-stack price and listing tracker. This MVP collects books from Books to Scrape, stores their current prices and every historical observation, and makes scrape health visible in a clean dashboard.

> **Track prices. Catch deals.**

## What is included

- Product catalogue with search and category filtering
- Product detail pages with price-history table and chart
- Source management and manual scrape controls
- Polite, paginated Books to Scrape collector with delay and user agent
- Persistent scrape logs, failure details, and dashboard metrics
- Automatic Quartz scrape every six hours (configurable)
- PostgreSQL persistence and an initial EF Core migration
- Swagger/OpenAPI at `http://localhost:5000/swagger`
- Responsive Angular dashboard
- Docker Compose for the database, API, and web app

## Technology

The API uses .NET 8, ASP.NET Core minimal APIs, EF Core 8, Npgsql, HtmlAgilityPack, Quartz.NET, and Swagger. The browser app uses Angular 17 and RxJS. PostgreSQL 16 stores application data.

The backend follows a clean split:

```text
backend/
  Trackly.Domain          Entities and core data model
  Trackly.Application     Use-case contracts and shared models
  Trackly.Infrastructure  EF Core, PostgreSQL, scraping, scheduling
  Trackly.Api             HTTP endpoints and composition root
frontend/
  trackly-web             Angular application
```

## Quick start with Docker

Prerequisites: Docker Desktop with Compose.

```bash
docker compose up --build
```

The API applies its migration at startup. Open the dashboard at `http://localhost:4200`, Swagger at `http://localhost:5000/swagger`, or health status at `http://localhost:5000/health`.

## Local development

Prerequisites: .NET 8 SDK, Node.js 20+, npm, PostgreSQL 16, and optionally the EF CLI (`dotnet tool install --global dotnet-ef --version 8.*`).

1. Start only PostgreSQL:

   ```bash
   docker compose up -d postgres
   ```

2. Apply the migration (the API also does this automatically on startup):

   ```bash
   dotnet ef database update --project backend/Trackly.Infrastructure --startup-project backend/Trackly.Api
   ```

3. Run the API:

   ```bash
   dotnet run --project backend/Trackly.Api
   ```

4. In a second terminal, install and run Angular:

   ```bash
   cd frontend/trackly-web
   npm install
   npm start
   ```

5. Visit `http://localhost:4200`. Open **Sources** and choose **Run scrape**. The request observes the configured delay between pages, then the dashboard and catalogue update.

The default database values are database `tracklydb`, user `postgres`, password `password`, and port `5432`. Override the API connection without changing source code:

```bash
ConnectionStrings__TracklyDatabase='Host=localhost;Port=5432;Database=tracklydb;Username=postgres;Password=your-password' dotnet run --project backend/Trackly.Api
```

Scraping behavior is under `Scraper` in `appsettings.json`. `MaxPages`, `DelayMilliseconds`, `UserAgent`, and the Quartz cron `Schedule` can all be overridden with environment variables such as `Scraper__MaxPages=5`.

## API map

- `GET/POST /api/sources`, `PUT/DELETE /api/sources/{id}`
- `GET /api/products`, `GET/DELETE /api/products/{id}`
- `GET /api/products/{id}/price-history`
- `POST /api/scraping/run/{sourceId}`
- `GET /api/scraping/jobs`, `GET /api/scraping/jobs/{id}`
- `GET /api/dashboard/summary`

## Future improvements

Useful next steps include authenticated user workspaces, price-drop alerts, retry policies, richer category extraction, background job persistence, configurable schedules in the UI, pagination, scraper adapters for additional sites, and automated unit/integration tests.

## Responsible scraping

Books to Scrape is intentionally provided as a safe demonstration target. Real-world collection must respect each site's `robots.txt`, Terms of Service, copyright and database rights, rate limits, privacy rules, and applicable law. Identify your client where appropriate, cache responses, request slowly, collect only necessary data, and stop when a site asks you to. Permission is better than cleverness.
