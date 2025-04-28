#!/bin/bash
set -e
docker build -t frontend:v1 -f Dockerfile .
docker run --rm --name frontend -p 3000:80 frontend:v1
