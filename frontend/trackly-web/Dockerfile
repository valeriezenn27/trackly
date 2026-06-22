FROM node:20-alpine AS build
WORKDIR /app
COPY frontend/trackly-web/package*.json ./
RUN npm ci
COPY frontend/trackly-web .
RUN npm run build
FROM nginx:alpine
COPY --from=build /app/dist/trackly-web/browser /usr/share/nginx/html
COPY frontend/trackly-web/nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
