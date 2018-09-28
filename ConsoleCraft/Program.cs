using System;
using System.Text;

namespace ConsoleCraft {
	class Program {
		int height = 0;
		int width = 0;
		string[] fb;
		const char black = ' ';
		const char white = '█';
		const char light_grey = '▓';
		const char middle_grey = '▒';
		const char dark_grey = '░';
		double[][] blockMap = new double[1][];
		double[] camRot = {0,0};
		double[] camPos = {0,0,0};

		public void init_cc(int w, int h) {
			fb = new string[h];
			height = h;
			width = w;
			blockMap[0] = new double[3];
			blockMap [0] [0] = 0;
			blockMap [0] [1] = 0;
			blockMap [0] [2] = 2;
			cc_loop ();
		}

		double my_sin(double x) {
			return Math.Sin ((x * Math.PI) / 180);
		}

		double my_cos(double x) {
			return Math.Cos ((x * Math.PI) / 180);
		}

		void cc_loop() {
			do {
				while (! Console.KeyAvailable) {
					cc_main();
				}
				ConsoleKey key = Console.ReadKey(true).Key;
				if (key == ConsoleKey.LeftArrow) {
					camRot[1] -= 1;
				} else if (key == ConsoleKey.RightArrow) {
					camRot[1] += 1;
				} else if (key == ConsoleKey.UpArrow) {
					camRot[0] += 1;
				} else if (key == ConsoleKey.DownArrow) {
					camRot[0] -= 1;
				} else if (key == ConsoleKey.W) {
					camPos[0] = camPos[0]+(my_sin(camRot[1])*0.1);
					camPos[2] = camPos[2]+(my_cos(camRot[1])*0.1);
				} else if (key == ConsoleKey.S) {
					camPos[0] = camPos[0]+(my_sin(camRot[1])*(-0.1));
					camPos[2] = camPos[2]+(my_cos(camRot[1])*(-0.1));
				}
			} while (0 == 0);
		}

		void cc_main() {
			init_fb ();
			if (camRot[1] > 360) {
				camRot[1] -= 360;
			} else if (camRot[1] < 0) {
				camRot[1] += 360;
			}
			if (camRot[0] > 360) {
				camRot[0] -= 360;
			} else if (camRot[0] < 0) {
				camRot[0] += 360;
			}
			foreach (double[] block in blockMap) {
				double[] p0 = { block [0] - 0.5f, block [1] + 0.5f, block [2] - 0.5f };
				double[] p1 = { block [0] - 0.5f, block [1] - 0.5f, block [2] - 0.5f };
				double[] p2 = { block [0] + 0.5f, block [1] + 0.5f, block [2] - 0.5f };
				double[] p3 = { block [0] + 0.5f, block [1] - 0.5f, block [2] - 0.5f };
				double[] p4 = { block [0] + 0.5f, block [1] + 0.5f, block [2] + 0.5f };
				double[] p5 = { block [0] + 0.5f, block [1] - 0.5f, block [2] + 0.5f };
				double[] p6 = { block [0] - 0.5f, block [1] + 0.5f, block [2] + 0.5f };
				double[] p7 = { block [0] - 0.5f, block [1] - 0.5f, block [2] + 0.5f };
				double[] block_tmp = {block[0],block[1],block[2]};
				for (int i = 0; i < 9; i++) {
					double[] pTmp = { 0, 0, 0 };
					if (i == 0) {
						pTmp = p0;
					} else if (i == 1) {
						pTmp = p1;
					} else if (i == 2) {
						pTmp = p2;
					} else if (i == 3) {
						pTmp = p3;
					} else if (i == 4) {
						pTmp = p4;
					} else if (i == 5) {
						pTmp = p5;
					} else if (i == 6) {
						pTmp = p6;
					} else if (i == 7) {
						pTmp = p7;
					} else if (i == 8) {
						pTmp = block;
					}
					double blockPosXRTC = ((my_cos (camRot [1]) * (pTmp [0] - camPos [0])) - (my_sin (camRot [1]) * (pTmp [2] - camPos [2])));
					double blockPosZRTC = ((my_cos (camRot [1]) * (pTmp [2] - camPos [2])) + (my_sin (camRot [1]) * (pTmp [0] - camPos [0])));
					double blockPosYRTC = -((my_cos (camRot [0]) * (pTmp [1] - camPos [1])) - (my_sin (camRot [0]) * blockPosZRTC));
					blockPosZRTC = ((my_cos (camRot [0]) * blockPosZRTC) + (my_sin (camRot [0]) * (pTmp [1] - camPos [1])));
					/*if (i == 8) {
						Console.WriteLine (" ");
						Console.WriteLine (camPos [0].ToString () + " " + camPos [1].ToString () + " " + camPos [2].ToString ());
						Console.WriteLine (camRot [0].ToString () + " " + camRot [1].ToString ());
						Console.WriteLine (pTmp [0].ToString () + " " + pTmp [1].ToString () + " " + pTmp [2].ToString ());
						Console.WriteLine (block_tmp [0].ToString () + " " + block_tmp [1].ToString () + " " + block_tmp [2].ToString ());
						Console.WriteLine (blockPosXRTC.ToString ());
						Console.WriteLine (blockPosYRTC.ToString ());
						Console.WriteLine (blockPosZRTC.ToString ());
					}*/
					if (i == 0) {
						p0 [0] = blockPosXRTC;
						p0 [1] = blockPosYRTC;
						p0 [2] = blockPosZRTC;
					} else if (i == 1) {
						p1 [0] = blockPosXRTC;
						p1 [1] = blockPosYRTC;
						p1 [2] = blockPosZRTC;
					} else if (i == 2) {
						p2 [0] = blockPosXRTC;
						p2 [1] = blockPosYRTC;
						p2 [2] = blockPosZRTC;
					} else if (i == 3) {
						p3 [0] = blockPosXRTC;
						p3 [1] = blockPosYRTC;
						p3 [2] = blockPosZRTC;
					} else if (i == 4) {
						p4 [0] = blockPosXRTC;
						p4 [1] = blockPosYRTC;
						p4 [2] = blockPosZRTC;
					} else if (i == 5) {
						p5 [0] = blockPosXRTC;
						p5 [1] = blockPosYRTC;
						p5 [2] = blockPosZRTC;
					} else if (i == 6) {
						p6 [0] = blockPosXRTC;
						p6 [1] = blockPosYRTC;
						p6 [2] = blockPosZRTC;
					} else if (i == 7) {
						p7 [0] = blockPosXRTC;
						p7 [1] = blockPosYRTC;
						p7 [2] = blockPosZRTC;
					} else if (i == 8) {
						block_tmp [0] = blockPosXRTC;
						block_tmp [1] = blockPosYRTC;
						block_tmp [2] = blockPosZRTC;
					}
				}
				if (block_tmp [0] / block_tmp [2] > -1.25f && block_tmp [0] / block_tmp [2] < 1.25f && block_tmp [1] / block_tmp [2] > -1f && block_tmp [1] / block_tmp [2] < 1f && block_tmp [2] > 0) {
					int[] p00 = {
						Convert.ToInt32 ((width / 4) + ((p0 [0] / p0 [2]) * (height / 2))),
						Convert.ToInt32 ((height / 2) + ((p0 [1] / p0 [2]) * (height / 2)))
					};
					int[] p01 = {
						Convert.ToInt32 ((width / 4) + ((p1 [0] / p1 [2]) * (height / 2))),
						Convert.ToInt32 ((height / 2) + ((p1 [1] / p1 [2]) * (height / 2)))
					};
					int[] p02 = {
						Convert.ToInt32 ((width / 4) + ((p2 [0] / p2 [2]) * (height / 2))),
						Convert.ToInt32 ((height / 2) + ((p2 [1] / p2 [2]) * (height / 2)))
					};
					int[] p03 = {
						Convert.ToInt32 ((width / 4) + ((p3 [0] / p3 [2]) * (height / 2))),
						Convert.ToInt32 ((height / 2) + ((p3 [1] / p3 [2]) * (height / 2)))
					};
					int[] p04 = {
						Convert.ToInt32 ((width / 4) + ((p4 [0] / p4 [2]) * (height / 2))),
						Convert.ToInt32 ((height / 2) + ((p4 [1] / p4 [2]) * (height / 2)))
					};
					int[] p05 = {
						Convert.ToInt32 ((width / 4) + ((p5 [0] / p5 [2]) * (height / 2))),
						Convert.ToInt32 ((height / 2) + ((p5 [1] / p5 [2]) * (height / 2)))
					};
					int[] p06 = {
						Convert.ToInt32 ((width / 4) + ((p6 [0] / p6 [2]) * (height / 2))),
						Convert.ToInt32 ((height / 2) + ((p6 [1] / p6 [2]) * (height / 2)))
					};
					int[] p07 = {
						Convert.ToInt32 ((width / 4) + ((p7 [0] / p7 [2]) * (height / 2))),
						Convert.ToInt32 ((height / 2) + ((p7 [1] / p7 [2]) * (height / 2)))
					};
					drawLine (p00 [0], p00 [1], p01 [0], p01 [1], white);
					drawLine (p00 [0], p00 [1], p02 [0], p02 [1], white);
					drawLine (p01 [0], p01 [1], p03 [0], p03 [1], white);
					drawLine (p02 [0], p02 [1], p03 [0], p03 [1], white);
					drawLine (p02 [0], p02 [1], p04 [0], p04 [1], white);
					drawLine (p03 [0], p03 [1], p05 [0], p05 [1], white);
					drawLine (p04 [0], p04 [1], p05 [0], p05 [1], white);
					drawLine (p04 [0], p04 [1], p06 [0], p06 [1], white);
					drawLine (p05 [0], p05 [1], p07 [0], p07 [1], white);
					drawLine (p06 [0], p06 [1], p07 [0], p07 [1], white);
					drawLine (p06 [0], p06 [1], p00 [0], p00 [1], white);
					drawLine (p07 [0], p07 [1], p01 [0], p01 [1], white);
					/*Console.WriteLine (p00 [0].ToString () + " " + p00 [1].ToString ());
					Console.WriteLine (p01 [0].ToString () + " " + p01 [1].ToString ());
					Console.WriteLine (p02 [0].ToString () + " " + p02 [1].ToString ());
					Console.WriteLine (p03 [0].ToString () + " " + p03 [1].ToString ());
					Console.WriteLine (p04 [0].ToString () + " " + p04 [1].ToString ());
					Console.WriteLine (p05 [0].ToString () + " " + p05 [1].ToString ());
					Console.WriteLine (p06 [0].ToString () + " " + p06 [1].ToString ());
					Console.WriteLine (p07 [0].ToString () + " " + p07 [1].ToString ());
					Console.WriteLine (" ");*/
				}
			}
			write_fb ();
			System.Threading.Thread.Sleep (100);
			//cc_main ();
		}

		void init_fb() {
			for (int i = 0; i < height; i++) {
				fb [i] = "";
				for (int i2 = 0; i2 < width; i2++) {
					fb [i] += " ";
				}
			}
		}

		void setPixel(int x, int y, char color) {
			if (y >= 0 && y <= height && (x*2) >= 0 && ((x*2)+1) <= width) {
				StringBuilder tmp_fb = new StringBuilder(fb [y]);
				tmp_fb [2 * x] = color;
				tmp_fb [(2 * x) + 1] = color;
				fb [y] = tmp_fb.ToString ();
			}
		}

		void drawLine(int x1, int y1, int x2, int y2, char color) {
			if (x1 > x2) {
				int xTmp = x1;
				x1 = x2;
				x2 = xTmp;
				int yTmp = y1;
				y1 = y2;
				y2 = yTmp;
			}
			int dx = x2 - x1;
			int dy = y2 - y1;
			if (x1 == x2) {
				if (y1 < y2) {
					for (int y = y1; y <= y2; y++) {
						setPixel(x1, y, color);
					}
				} else if (y1 == y2) {
					setPixel(x1, y1, color);
				} else if (y1 > y2) {
					for (int y = y2; y <= y1; y++) {
						setPixel(x1, y, color);
					}
				}
			} else if (dx >= dy) {
				for (int x = x1; x <= x2; x++) {
					int y = y1 + (dy * (x - x1) / dx);
					setPixel(x, y, color);
				}
			} else {
				for (int y = y1; y <= y2; y++) {
					int x = x1 + (dx * (y - y1) / dy);
					setPixel(x, y, color);
				}
			}
		}

		void write_fb() {
			foreach (string line in fb) {
				Console.WriteLine (line);
			}
		}

		public static void Main (string[] args) {
			Program p = new Program ();
			p.init_cc (int.Parse(args[0]), int.Parse(args[1]));
		}
	}
}
