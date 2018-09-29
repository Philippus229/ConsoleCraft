using System;
using System.Text;

namespace ConsoleCraft {
	class Program {
		int height = 0;
		int width = 0;
		const int worldSize = 256;
		const int worldHeight = 128;
		string[] fb;
		const char black = ' ';
		const char white = '█';
		const char light_grey = '▓';
		const char middle_grey = '▒';
		const char dark_grey = '░';
		int[][][] cubeMap = new int[worldHeight][][];
		double[] camRot = {0,0};
		double[] camPos = {0,0,0};
		const int fov = 12;

		public void init_cc(int w, int h) {
			fb = new string[h];
			for (int i = 0; i < cubeMap.Length; i++) {
				cubeMap [i] = new int[worldSize][];
				for (int i2 = 0; i2 < cubeMap [i].Length; i2++) {
					cubeMap [i] [i2] = new int[worldSize];
					for (int i3 = 0; i3 < cubeMap [i] [i2].Length; i3++) {
						cubeMap [i] [i2] [i3] = 0;
					}
				}
			}
			setCube(0, 0, 4, 1);
			/*setCube(1, -1, 3, 1);
			setCube(1, -1, 5, 1);
			setCube(-1, -1, 3, 1);
			setCube(-1, -1, 5, 1);
			setCube(1, 1, 3, 1);
			setCube(1, 1, 5, 1);
			setCube(-1, 1, 3, 1);
			setCube(-1, 1, 5, 1);*/
			int y2 = -2;
			for (int x2 = -(worldSize / 2); x2 <= (worldSize / 2); x2++) {
				for (int z2 = -(worldSize / 2); z2 <= (worldSize / 2); z2++) {
					setCube(x2, y2, z2, 1);
				}
			}
			height = h;
			width = w;
			cc_loop ();
		}

		void setCube(int x, int y, int z, int blockType) {
			if ((y + (worldHeight / 2)) >= 0 && (y + (worldHeight / 2)) < worldHeight && (x + (worldSize /  2)) >= 0 && (x + (worldSize / 2)) < worldSize && (z + (worldSize / 2)) >= 0 && (z + (worldSize / 2)) < worldSize) {
				cubeMap[y + (worldHeight / 2)][x + (worldSize / 2)][z + (worldSize / 2)] = blockType;
			}
		}

		double sin(double x) {
			return Math.Sin ((x * Math.PI) / 180);
		}

		double cos(double x) {
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
					camPos[0] += (sin(camRot[1])*0.1);
					camPos[2] += (cos(camRot[1])*0.1);
				} else if (key == ConsoleKey.S) {
					camPos[0] += (sin(camRot[1])*(-0.1));
					camPos[2] += (cos(camRot[1])*(-0.1));
				}
			} while (0 == 0);
		}

		void renderMap() {
			int camPosXR = Convert.ToInt32(camPos[0]);
			int camPosYR = Convert.ToInt32(camPos[1]);
			int camPosZR = Convert.ToInt32(camPos[2]);
			for (int bR = 0; bR < 8; bR++) {
				for (int y = -(fov / 2); y < 0; y++) {
					for (int x = -(fov / 2); x < 0; x++) {
						for (int z = -(fov / 2); z < 0; z++) {
							if ((camPosXR + x) >= -(worldSize / 2) && (camPosXR + x) <= (worldSize / 2) && (camPosYR + y) >= -(worldHeight / 2) && (camPosYR + y) <= (worldHeight / 2) && (camPosZR + z) >= -(worldSize / 2) && (camPosZR + z) <= (worldSize / 2)) {
								if (bR == 0) {
									if (cubeMap[(camPosYR + y) + (worldHeight / 2)][(camPosXR + x) + (worldSize / 2)][(camPosZR + z) + (worldSize / 2)] != 0) {
										renderCube((camPosXR + x), (camPosYR + y), (camPosZR + z), cubeMap[(camPosYR + y) + (worldHeight / 2)][(camPosXR + x) + (worldSize / 2)][(camPosZR + z) + (worldSize / 2)]);
									}
								} else if (bR == 1) {
									if (cubeMap[(camPosYR + y) + (worldHeight / 2)][(camPosXR + x) + (worldSize / 2)][(camPosZR - z) + (worldSize / 2)] != 0) {
										renderCube((camPosXR + x), (camPosYR + y), (camPosZR - z), cubeMap[(camPosYR + y) + (worldHeight / 2)][(camPosXR + x) + (worldSize / 2)][(camPosZR - z) + (worldSize / 2)]);
									}
								} else if (bR == 2) {
									if (cubeMap[(camPosYR + y) + (worldHeight / 2)][(camPosXR - x) + (worldSize / 2)][(camPosZR - z) + (worldSize / 2)] != 0) {
										renderCube((camPosXR - x), (camPosYR + y), (camPosZR - z), cubeMap[(camPosYR + y) + (worldHeight / 2)][(camPosXR - x) + (worldSize / 2)][(camPosZR - z) + (worldSize / 2)]);
									}
								} else if (bR == 3) {
									if (cubeMap[(camPosYR + y) + (worldHeight / 2)][(camPosXR - x) + (worldSize / 2)][(camPosZR + z) + (worldSize / 2)] != 0) {
										renderCube((camPosXR - x), (camPosYR + y), (camPosZR + z), cubeMap[(camPosYR + y) + (worldHeight / 2)][(camPosXR - x) + (worldSize / 2)][(camPosZR + z) + (worldSize / 2)]);
									}
								} else if (bR == 4) {
									if (cubeMap[(camPosYR - y) + (worldHeight / 2)][(camPosXR + x) + (worldSize / 2)][(camPosZR + z) + (worldSize / 2)] != 0) {
										renderCube((camPosXR + x), (camPosYR - y), (camPosZR + z), cubeMap[(camPosYR - y) + (worldHeight / 2)][(camPosXR + x) + (worldSize / 2)][(camPosZR + z) + (worldSize / 2)]);
									}
								} else if (bR == 5) {
									if (cubeMap[(camPosYR - y) + (worldHeight / 2)][(camPosXR + x) + (worldSize / 2)][(camPosZR - z) + (worldSize / 2)] != 0) {
										renderCube((camPosXR + x), (camPosYR - y), (camPosZR - z), cubeMap[(camPosYR - y) + (worldHeight / 2)][(camPosXR + x) + (worldSize / 2)][(camPosZR - z) + (worldSize / 2)]);
									}
								} else if (bR == 6) {
									if (cubeMap[(camPosYR - y) + (worldHeight / 2)][(camPosXR - x) + (worldSize / 2)][(camPosZR - z) + (worldSize / 2)] != 0) {
										renderCube((camPosXR - x), (camPosYR - y), (camPosZR - z), cubeMap[(camPosYR - y) + (worldHeight / 2)][(camPosXR - x) + (worldSize / 2)][(camPosZR - z) + (worldSize / 2)]);
									}
								} else if (bR == 7) {
									if (cubeMap[(camPosYR - y) + (worldHeight / 2)][(camPosXR - x) + (worldSize / 2)][(camPosZR + z) + (worldSize / 2)] != 0) {
										renderCube((camPosXR - x), (camPosYR - y), (camPosZR + z), cubeMap[(camPosYR - y) + (worldHeight / 2)][(camPosXR - x) + (worldSize / 2)][(camPosZR + z) + (worldSize / 2)]);
									}
								}
							}
						}
					}
				}
			}
			for (int bR = 0; bR < 4; bR++) {
				for (int y = -(fov / 2); y < 0; y++) {
					for (int x = -(fov / 2); x < 0; x++) {
						if ((camPosXR + x) >= -(worldSize / 2) && (camPosXR + x) <= (worldSize / 2) && (camPosYR + y) >= -(worldHeight / 2) && (camPosYR + y) <= (worldHeight / 2) && (camPosZR) >= -(worldSize / 2) && (camPosZR) <= (worldSize / 2)) {
							if (bR == 0) {
								if (cubeMap[(camPosYR - y) + (worldHeight / 2)][(camPosXR - x) + (worldSize / 2)][(camPosZR) + (worldSize / 2)] != 0) {
									renderCube((camPosXR - x), (camPosYR - y), (camPosZR), cubeMap[(camPosYR - y) + (worldHeight / 2)][(camPosXR - x) + (worldSize / 2)][(camPosZR) + (worldSize / 2)]);
								}
							} else if (bR == 1) {
								if (cubeMap[(camPosYR + y) + (worldHeight / 2)][(camPosXR - x) + (worldSize / 2)][(camPosZR) + (worldSize / 2)] != 0) {
									renderCube((camPosXR - x), (camPosYR + y), (camPosZR), cubeMap[(camPosYR + y) + (worldHeight / 2)][(camPosXR - x) + (worldSize / 2)][(camPosZR) + (worldSize / 2)]);
								}
							} else if (bR == 2) {
								if (cubeMap[(camPosYR + y) + (worldHeight / 2)][(camPosXR + x) + (worldSize / 2)][(camPosZR) + (worldSize / 2)] != 0) {
									renderCube((camPosXR + x), (camPosYR + y), (camPosZR), cubeMap[(camPosYR + y) + (worldHeight / 2)][(camPosXR + x) + (worldSize / 2)][(camPosZR) + (worldSize / 2)]);
								}
							} else if (bR == 3) {
								if (cubeMap[(camPosYR - y) + (worldHeight / 2)][(camPosXR + x) + (worldSize / 2)][(camPosZR) + (worldSize / 2)] != 0) {
									renderCube((camPosXR + x), (camPosYR - y), (camPosZR), cubeMap[(camPosYR - y) + (worldHeight / 2)][(camPosXR + x) + (worldSize / 2)][(camPosZR) + (worldSize / 2)]);
								}
							}
						}
					}
				}
			}
			for (int bR = 0; bR < 4; bR++) {
				for (int y = -(fov / 2); y < 0; y++) {
					for (int z = -(fov / 2); z < 0; z++) {
						if ((camPosXR) >= -(worldSize / 2) && (camPosXR) <= (worldSize / 2) && (camPosYR + y) >= -(worldHeight / 2) && (camPosYR + y) <= (worldHeight / 2) && (camPosZR + z) >= -(worldSize / 2) && (camPosZR + z) <= (worldSize / 2)) {
							if (bR == 0) {
								if (cubeMap[(camPosYR - y) + (worldHeight / 2)][(camPosXR) + (worldSize / 2)][(camPosZR - z) + (worldSize / 2)] != 0) {
									renderCube((camPosXR), (camPosYR - y), (camPosZR - z), cubeMap[(camPosYR - y) + (worldHeight / 2)][(camPosXR) + (worldSize / 2)][(camPosZR - z) + (worldSize / 2)]);
								}
							} else if (bR == 1) {
								if (cubeMap[(camPosYR + y) + (worldHeight / 2)][(camPosXR) + (worldSize / 2)][(camPosZR - z) + (worldSize / 2)] != 0) {
									renderCube((camPosXR), (camPosYR + y), (camPosZR - z), cubeMap[(camPosYR + y) + (worldHeight / 2)][(camPosXR) + (worldSize / 2)][(camPosZR - z) + (worldSize / 2)]);
								}
							} else if (bR == 2) {
								if (cubeMap[(camPosYR + y) + (worldHeight / 2)][(camPosXR) + (worldSize / 2)][(camPosZR + z) + (worldSize / 2)] != 0) {
									renderCube((camPosXR), (camPosYR + y), (camPosZR + z), cubeMap[(camPosYR + y) + (worldHeight / 2)][(camPosXR) + (worldSize / 2)][(camPosZR + z) + (worldSize / 2)]);
								}
							} else if (bR == 3) {
								if (cubeMap[(camPosYR - y) + (worldHeight / 2)][(camPosXR) + (worldSize / 2)][(camPosZR + z) + (worldSize / 2)] != 0) {
									renderCube((camPosXR), (camPosYR - y), (camPosZR + z), cubeMap[(camPosYR - y) + (worldHeight / 2)][(camPosXR) + (worldSize / 2)][(camPosZR + z) + (worldSize / 2)]);
								}
							}
						}
					}
				}
			}
			for (int bR = 0; bR < 4; bR++) {
				for (int x = -(fov / 2); x < 0; x++) {
					for (int z = -(fov / 2); z < 0; z++) {
						if ((camPosXR + x) >= -(worldSize / 2) && (camPosXR + x) <= (worldSize / 2) && (camPosYR) >= -(worldHeight / 2) && (camPosYR) <= (worldHeight / 2) && (camPosZR + z) >= -(worldSize / 2) && (camPosZR + z) <= (worldSize / 2)) {
							if (bR == 0) {
								if (cubeMap[(camPosYR) + (worldHeight / 2)][(camPosXR - x) + (worldSize / 2)][(camPosZR - z) + (worldSize / 2)] != 0) {
									renderCube((camPosXR - x), (camPosYR), (camPosZR - z), cubeMap[(camPosYR) + (worldHeight / 2)][(camPosXR - x) + (worldSize / 2)][(camPosZR - z) + (worldSize / 2)]);
								}
							} else if (bR == 1) {
								if (cubeMap[(camPosYR) + (worldHeight / 2)][(camPosXR + x) + (worldSize / 2)][(camPosZR - z) + (worldSize / 2)] != 0) {
									renderCube((camPosXR + x), (camPosYR), (camPosZR - z), cubeMap[(camPosYR) + (worldHeight / 2)][(camPosXR + x) + (worldSize / 2)][(camPosZR - z) + (worldSize / 2)]);
								}
							} else if (bR == 2) {
								if (cubeMap[(camPosYR) + (worldHeight / 2)][(camPosXR + x) + (worldSize / 2)][(camPosZR + z) + (worldSize / 2)] != 0) {
									renderCube((camPosXR + x), (camPosYR), (camPosZR + z), cubeMap[(camPosYR) + (worldHeight / 2)][(camPosXR + x) + (worldSize / 2)][(camPosZR + z) + (worldSize / 2)]);
								}
							} else if (bR == 3) {
								if (cubeMap[(camPosYR) + (worldHeight / 2)][(camPosXR - x) + (worldSize / 2)][(camPosZR + z) + (worldSize / 2)] != 0) {
									renderCube((camPosXR - x), (camPosYR), (camPosZR + z), cubeMap[(camPosYR) + (worldHeight / 2)][(camPosXR - x) + (worldSize / 2)][(camPosZR + z) + (worldSize / 2)]);
								}
							}
						}
					}
				}
			}
			for (int bR = 0; bR < 2; bR++) {
				for (int x = -(fov / 2); x < 0; x++) {
					if ((camPosXR + x) >= -(worldSize / 2) && (camPosXR + x) <= (worldSize / 2) && (camPosYR) >= -(worldHeight / 2) && (camPosYR) <= (worldHeight / 2) && (camPosZR) >= -(worldSize / 2) && (camPosZR) <= (worldSize / 2)) {
						if (bR == 0) {
							if (cubeMap[(camPosYR) + (worldHeight / 2)][(camPosXR + x) + (worldSize / 2)][(camPosZR) + (worldSize / 2)] != 0) {
								renderCube((camPosXR + x), (camPosYR), (camPosZR), cubeMap[(camPosYR) + (worldHeight / 2)][(camPosXR + x) + (worldSize / 2)][(camPosZR) + (worldSize / 2)]);
							}
						} else if (bR == 1) {
							if (cubeMap[(camPosYR) + (worldHeight / 2)][(camPosXR - x) + (worldSize / 2)][(camPosZR) + (worldSize / 2)] != 0) {
								renderCube((camPosXR - x), (camPosYR), (camPosZR), cubeMap[(camPosYR) + (worldHeight / 2)][(camPosXR - x) + (worldSize / 2)][(camPosZR) + (worldSize / 2)]);
							}
						}
					}
				}
			}
			for (int bR = 0; bR < 2; bR++) {
				for (int y = -(fov / 2); y < 0; y++) {
					if ((camPosXR) >= -(worldSize / 2) && (camPosXR) <= (worldSize / 2) && (camPosYR + y) >= -(worldHeight / 2) && (camPosYR + y) <= (worldHeight / 2) && (camPosZR) >= -(worldSize / 2) && (camPosZR) <= (worldSize / 2)) {
						if (bR == 0) {
							if (cubeMap[(camPosYR + y) + (worldHeight / 2)][(camPosXR) + (worldSize / 2)][(camPosZR) + (worldSize / 2)] != 0) {
								renderCube((camPosXR), (camPosYR + y), (camPosZR), cubeMap[(camPosYR + y) + (worldHeight / 2)][(camPosXR) + (worldSize / 2)][(camPosZR) + (worldSize / 2)]);
							}
						} else if (bR == 1) {
							if (cubeMap[(camPosYR - y) + (worldHeight / 2)][(camPosXR) + (worldSize / 2)][(camPosZR) + (worldSize / 2)] != 0) {
								renderCube((camPosXR), (camPosYR - y), (camPosZR), cubeMap[(camPosYR - y) + (worldHeight / 2)][(camPosXR) + (worldSize / 2)][(camPosZR) + (worldSize / 2)]);
							}
						}
					}
				}
			}
			for (int bR = 0; bR < 2; bR++) {
				for (int z = -(fov / 2); z < 0; z++) {
					if ((camPosXR) >= -(worldSize / 2) && (camPosXR) <= (worldSize / 2) && (camPosYR) >= -(worldHeight / 2) && (camPosYR) <= (worldHeight / 2) && (camPosZR + z) >= -(worldSize / 2) && (camPosZR + z) <= (worldSize / 2)) {
						if (bR == 0) {
							if (cubeMap[(camPosYR) + (worldHeight / 2)][(camPosXR) + (worldSize / 2)][(camPosZR + z) + (worldSize / 2)] != 0) {
								renderCube((camPosXR), (camPosYR), (camPosZR + z), cubeMap[(camPosYR) + (worldHeight / 2)][(camPosXR) + (worldSize / 2)][(camPosZR + z) + (worldSize / 2)]);
							}
						} else if (bR == 1) {
							if (cubeMap[(camPosYR) + (worldHeight / 2)][(camPosXR) + (worldSize / 2)][(camPosZR - z) + (worldSize / 2)] != 0) {
								renderCube((camPosXR), (camPosYR), (camPosZR - z), cubeMap[(camPosYR) + (worldHeight / 2)][(camPosXR) + (worldSize / 2)][(camPosZR - z) + (worldSize / 2)]);
							}
						}
					}
				}
			}
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
			renderMap ();
			write_fb ();
			System.Threading.Thread.Sleep (50);
		}

		char dist_color(double[] p) {
			if (p [2] < 3) {
				return white;
			} else if (p [2] < 6) {
				return light_grey;
			} else if (p [2] < 9) {
				return middle_grey;
			} else if (p [2] < 12) {
				return dark_grey;
			} else {
				return black;
			}
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
			if (y >= 0 && y < height && (x*2) >= 0 && ((x*2)+1) < width) {
				StringBuilder tmp_fb = new StringBuilder(fb [y]);
				tmp_fb [2 * x] = color;
				tmp_fb [(2 * x) + 1] = color;
				fb [y] = tmp_fb.ToString ();
			}
		}

		void drawLine(int x0, int y0, int x1, int y1, char color) {
			bool running = true;
			int dx =  Math.Abs(x1-x0), sx = x0<x1 ? 1 : -1;
			int dy = -Math.Abs(y1-y0), sy = y0<y1 ? 1 : -1;
			int err = dx+dy, e2;
			while (running) {
				setPixel(x0,y0,color);
				if (x0==x1 && y0==y1) running = false;
				e2 = 2*err;
				if (e2 > dy) { err += dy; x0 += sx; }
				if (e2 < dx) { err += dx; y0 += sy; }
			}
		}

		void write_fb() {
			foreach (string line in fb) {
				Console.WriteLine (line);
			}
		}

		void renderCube(double posX, double posY, double posZ, int blockType) {
			int pXint = Convert.ToInt32(posX);
			int pYint = Convert.ToInt32(posY);
			int pZint = Convert.ToInt32(posZ);
			bool[] fTR = new bool[6];
			for (int i = 0; i < 6; i++) {
				fTR[i] = true;
			}
			if (posX < (worldSize / 2)) {
				if (cubeMap[pYint + (worldHeight / 2)][pXint + (worldSize / 2) + 1][pZint + (worldSize / 2)] != 0) {
					fTR[3] = false;
				}
			}
			if (posX < (worldSize / 2)) {
				if (cubeMap[pYint + (worldHeight / 2)][pXint + (worldSize / 2) - 1][pZint + (worldSize / 2)] != 0) {
					fTR[1] = false;
				}
			}
			if (posY < (worldSize / 2)) {
				if (cubeMap[pYint + (worldHeight / 2) + 1][pXint + (worldSize / 2)][pZint + (worldSize / 2)] != 0) {
					fTR[4] = false;
				}
			}
			if (posY < (worldSize / 2)) {
				if (cubeMap[pYint + (worldHeight / 2) - 1][pXint + (worldSize / 2)][pZint + (worldSize / 2)] != 0) {
					fTR[5] = false;
				}
			}
			if (posZ < (worldSize / 2)) {
				if (cubeMap[pYint + (worldHeight / 2)][pXint + (worldSize / 2)][pZint + (worldSize / 2) + 1] != 0) {
					fTR[2] = false;
				}
			}
			if (posZ < (worldSize / 2)) {
				if (cubeMap[pYint + (worldHeight / 2)][pXint + (worldSize / 2)][pZint + (worldSize / 2) - 1] != 0) {
					fTR[0] = false;
				}
			}
			if (posX > camPos[0]) {
				fTR[3] = false;
			}
			if (posX < camPos[0]) {
				fTR[1] = false;
			}
			if (posY > camPos[1]) {
				fTR[4] = false;
			}
			if (posY < camPos[1]) {
				fTR[5] = false;
			}
			if (posZ > camPos[2]) {
				fTR[2] = false;
			}
			if (posZ < camPos[2]) {
				fTR[0] = false;
			}
			posX -= camPos[0];
			posY -= camPos[1];
			posZ -= camPos[2];
			double p0x = posX - 0.5;
			double p0y = posY + 0.5;
			double p0z = posZ - 0.5;
			double p1x = posX - 0.5;
			double p1y = posY - 0.5;
			double p1z = posZ - 0.5;
			double p2x = posX + 0.5;
			double p2y = posY + 0.5;
			double p2z = posZ - 0.5;
			double p3x = posX + 0.5;
			double p3y = posY - 0.5;
			double p3z = posZ - 0.5;
			double p4x = posX + 0.5;
			double p4y = posY + 0.5;
			double p4z = posZ + 0.5;
			double p5x = posX + 0.5;
			double p5y = posY - 0.5;
			double p5z = posZ + 0.5;
			double p6x = posX - 0.5;
			double p6y = posY + 0.5;
			double p6z = posZ + 0.5;
			double p7x = posX - 0.5;
			double p7y = posY - 0.5;
			double p7z = posZ + 0.5;
			double cubePosX = posX;
			double cubePosY = posY;
			double cubePosZ = posZ;
			bool p0set = false;
			bool p1set = false;
			bool p2set = false;
			bool p3set = false;
			bool p4set = false;
			bool p5set = false;
			bool p6set = false;
			bool p7set = false;
			for (int i = 0; i < 9; i++) {
				double pX = 0;
				double pY = 0;
				double pZ = 0;
				bool rTP = true;
				if (i == 0 && (fTR[0] || fTR[1] || fTR[4])) {
					pX = p0x;
					pY = p0y;
					pZ = p0z;
					p0set = true;
				} else if (i == 1 && (fTR[0] || fTR[1] || fTR[5])) {
					pX = p1x;
					pY = p1y;
					pZ = p1z;
					p1set = true;
				} else if (i == 2 && (fTR[3] || fTR[0] || fTR[4])) {
					pX = p2x;
					pY = p2y;
					pZ = p2z;
					p2set = true;
				} else if (i == 3 && (fTR[3] || fTR[0] || fTR[5])) {
					pX = p3x;
					pY = p3y;
					pZ = p3z;
					p3set = true;
				} else if (i == 4 && (fTR[2] || fTR[3] || fTR[4])) {
					pX = p4x;
					pY = p4y;
					pZ = p4z;
					p4set = true;
				} else if (i == 5 && (fTR[2] || fTR[3] || fTR[5])) {
					pX = p5x;
					pY = p5y;
					pZ = p5z;
					p5set = true;
				} else if (i == 6 && (fTR[1] || fTR[2] || fTR[4])) {
					pX = p6x;
					pY = p6y;
					pZ = p6z;
					p6set = true;
				} else if (i == 7 && (fTR[1] || fTR[2] || fTR[5])) {
					pX = p7x;
					pY = p7y;
					pZ = p7z;
					p7set = true;
				} else if (i == 8) {
					pX = cubePosX;
					pY = cubePosY;
					pZ = cubePosZ;
				} else {
					rTP = false;
				}
				double yPosRTC;
				double xPosRTC;
				double zPosRTC;
				double zPosRTC2;
				if (rTP == true) {
					xPosRTC = (cos(camRot[1]) * pX) - (sin(camRot[1]) * pZ);
					zPosRTC = (cos(camRot[1]) * pZ) + (sin(camRot[1]) * pX);
					yPosRTC = -((cos(camRot[0]) * pY) - (sin(camRot[0]) * zPosRTC));
					zPosRTC2 = (cos(camRot[0]) * zPosRTC) + (sin(camRot[0]) * pY);
					if (i == 0) {
						p0x = xPosRTC;
						p0y = yPosRTC;
						p0z = zPosRTC2;
					} else if (i == 1) {
						p1x = xPosRTC;
						p1y = yPosRTC;
						p1z = zPosRTC2;
					} else if (i == 2) {
						p2x = xPosRTC;
						p2y = yPosRTC;
						p2z = zPosRTC2;
					} else if (i == 3) {
						p3x = xPosRTC;
						p3y = yPosRTC;
						p3z = zPosRTC2;
					} else if (i == 4) {
						p4x = xPosRTC;
						p4y = yPosRTC;
						p4z = zPosRTC2;
					} else if (i == 5) {
						p5x = xPosRTC;
						p5y = yPosRTC;
						p5z = zPosRTC2;
					} else if (i == 6) {
						p6x = xPosRTC;
						p6y = yPosRTC;
						p6z = zPosRTC2;
					} else if (i == 7) {
						p7x = xPosRTC;
						p7y = yPosRTC;
						p7z = zPosRTC2;
					} else if (i == 8) {
						cubePosX = xPosRTC;
						cubePosY = yPosRTC;
						cubePosZ = zPosRTC2;
					}
				}
			}
			double[] pBlock = { cubePosX, cubePosY, cubePosZ };
			char color = dist_color(pBlock);
			bool renderIt = false;
			if (cubePosZ > 0 && cubePosZ < 12) {
				if ((cubePosX / cubePosZ) < 1.75 && (cubePosX / cubePosZ) > -1.75 && (cubePosY / cubePosZ) < 1 && (cubePosY / cubePosZ) > -1) {
					renderIt = true;
				}
			}
			if (renderIt == true) {
				double x0 = (width / 4) + ((p0x / p0z) * (height / 2));
				double x1 = (width / 4) + ((p1x / p1z) * (height / 2));;
				double x2 = (width / 4) + ((p2x / p2z) * (height / 2));;
				double x3 = (width / 4) + ((p3x / p3z) * (height / 2));;
				double x4 = (width / 4) + ((p4x / p4z) * (height / 2));;
				double x5 = (width / 4) + ((p5x / p5z) * (height / 2));;
				double x6 = (width / 4) + ((p6x / p6z) * (height / 2));;
				double x7 = (width / 4) + ((p7x / p7z) * (height / 2));;
				double y0 = (height / 2) + ((p0y / p0z) * (height / 2));;
				double y1 = (height / 2) + ((p1y / p1z) * (height / 2));;
				double y2 = (height / 2) + ((p2y / p2z) * (height / 2));;
				double y3 = (height / 2) + ((p3y / p3z) * (height / 2));;
				double y4 = (height / 2) + ((p4y / p4z) * (height / 2));;
				double y5 = (height / 2) + ((p5y / p5z) * (height / 2));;
				double y6 = (height / 2) + ((p6y / p6z) * (height / 2));;
				double y7 = (height / 2) + ((p7y / p7z) * (height / 2));;
				int x00 = Convert.ToInt32(x0);
				int x01 = Convert.ToInt32(x1);
				int x02 = Convert.ToInt32(x2);
				int x03 = Convert.ToInt32(x3);
				int x04 = Convert.ToInt32(x4);
				int x05 = Convert.ToInt32(x5);
				int x06 = Convert.ToInt32(x6);
				int x07 = Convert.ToInt32(x7);
				int y00 = Convert.ToInt32(y0);
				int y01 = Convert.ToInt32(y1);
				int y02 = Convert.ToInt32(y2);
				int y03 = Convert.ToInt32(y3);
				int y04 = Convert.ToInt32(y4);
				int y05 = Convert.ToInt32(y5);
				int y06 = Convert.ToInt32(y6);
				int y07 = Convert.ToInt32(y7);
				if (p0set && p1set) {
					drawLine(x00, y00, x01, y01, color);
				}
				if (p0set && p2set) {
					drawLine(x00, y00, x02, y02, color);
				}
				if (p1set && p3set) {
					drawLine(x01, y01, x03, y03, color);
				}
				if (p2set && p3set) {
					drawLine(x02, y02, x03, y03, color);
				}
				if (p2set && p4set) {
					drawLine(x02, y02, x04, y04, color);
				}
				if (p3set && p5set) {
					drawLine(x03, y03, x05, y05, color);
				}
				if (p4set && p5set) {
					drawLine(x04, y04, x05, y05, color);
				}
				if (p4set && p6set) {
					drawLine(x04, y04, x06, y06, color);
				}
				if (p5set && p7set) {
					drawLine(x05, y05, x07, y07, color);
				}
				if (p6set && p7set) {
					drawLine(x06, y06, x07, y07, color);
				}
				if (p6set && p0set) {
					drawLine(x06, y06, x00, y00, color);
				}
				if (p7set && p1set) {
					drawLine(x07, y07, x01, y01, color);
				}
				if (blockType == 2) {
					if (p0set && p3set) {
						drawLine(x00, y00, x03, y03, color);
					}
					if (p3set && p4set) {
						drawLine(x03, y03, x04, y04, color);
					}
					if (p4set && p7set) {
						drawLine(x04, y04, x07, y07, color);
					}
					if (p7set && p0set) {
						drawLine(x07, y07, x00, y00, color);
					}
					if (p0set && p4set) {
						drawLine(x00, y00, x04, y04, color);
					}
					if (p3set && p7set) {
						drawLine(x03, y03, x07, y07, color);
					}
				}
			}
		}

		public static void Main (string[] args) {
			Program p = new Program ();
			p.init_cc (int.Parse(args[0]), int.Parse(args[1]));
		}
	}
}
