// k6 smoke - activate endpoint
import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = { vus: 5, duration: '30s' };
const BASE = __ENV.BASE_URL || 'http://localhost:5080';

export default function () {
  const key = Math.random().toString(36).slice(2);
  const payload = JSON.stringify({ userId: '00000000-0000-0000-0000-000000000001', planId: 'basic' });
  const res = http.post(`${BASE}/api/v1/subscriptions/activate`, payload, {
    headers: { 'Content-Type': 'application/json', 'Idempotency-Key': key }
  });
  check(res, { 'status 200/202': r => r.status === 200 || r.status === 202 });
  sleep(1);
}
