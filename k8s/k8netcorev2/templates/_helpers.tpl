{{/* vim: set filetype=mustache: */}}

{{- define "env" -}}
{{ if .Values.global.msi.endpoint -}}
- name: MSI_ENDPOINT
  value: {{ .Values.global.msi.endpoint -}}
{{ end }}
{{ if .Values.global.msi.secret -}}
- name: MSI_SECRET
  value: {{ .Values.global.msi.secret -}}
{{ end }}

{{- end -}}}}
