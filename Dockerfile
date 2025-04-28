ARG APP_BUILD_DIR=/app/scratch
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
FROM base AS dev
ARG APP_BUILD_DIR
SHELL ["/bin/bash", "--login", "-c"]
RUN curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.35.3/install.sh | bash
RUN nvm install --lts
FROM dev AS build
COPY . ${APP_BUILD_DIR}
RUN cd ${APP_BUILD_DIR} && \
    npm install && \
    npm run build

FROM nginx:alpine AS prod
ARG APP_BUILD_DIR
COPY --from=build ${APP_BUILD_DIR}/dist/ /usr/share/nginx/html